using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Middleware;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Repository;
using IdealTimeTracker.API.Services;
using IdealTImeTracker.API.Repository;
using IdealTImeTracker.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}

                     }
                 });
    });

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddScoped<ITokenGenerate,TokenGenerationService>();

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Repo
builder.Services.AddScoped<IRepo<User, string>, UserRepo>();
builder.Services.AddScoped<IRepo<UserActivity, int>, UserActivityRepo>();
builder.Services.AddScoped<IBulkRepo<UserLog>, UserLogRepo>();
builder.Services.AddScoped<IApplicationConfigRepo, ApplicationConfigRepo>();
builder.Services.AddScoped<IAllUserRepo, AllUserRepo>();



// Service
builder.Services.AddScoped<IAdminAction, AdminAction>();
builder.Services.AddScoped<IUserAction, UserAction>();
builder.Services.AddScoped<IManagerAction, ManagerAction>();
builder.Services.AddScoped<IApplicationAction, ApplicationAction>();


// middleware

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
    dbContext.Database.EnsureCreated(); // Or use dbContext.Database.Migrate() depending on your needs

}
// Middleware

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseCors("MyCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
