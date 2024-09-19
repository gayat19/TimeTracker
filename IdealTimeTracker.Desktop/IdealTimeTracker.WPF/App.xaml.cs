using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Command;
using IdealTimeTracker.WPF.Context;
using IdealTimeTracker.WPF.Repository;
using IdealTimeTracker.WPF.Repository.Interface;
using IdealTimeTracker.WPF.Service;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;
using IdealTimeTracker.WPF.Utility.Hook;
using IdealTimeTracker.WPF.Utility.Options;
using IdealTimeTracker.WPF.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System.Windows;

namespace IdealTimeTracker.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((hostcontext,services) =>
            {
                string connectionString = hostcontext.Configuration.GetConnectionString("Default");
                services.AddSingleton(new UserContextDbFactory(connectionString));

                services.AddSingleton<IUserActivityRepo, UserActivityRepo>();
                services.AddSingleton<IUserLogRepo, UserLogRepo>();
                services.AddSingleton<IUserLogging, UserLogging>();
                services.AddSingleton<IUserRepo, UserRepo>();
                services.AddSingleton<IApplicationConfigRepo, ApplicationConfigRepo>();



                services.AddSingleton<ModalStore>();

                services.AddSingleton<IDisplayTimerStore,DisplayTimerStore>();
                services.AddSingleton<IActivityTimerStore, ActivityTimerStore>();

                services.AddSingleton<UserStore>();

                services.AddSingleton<ISyncDataService, SyncDataService>();


                services.AddSingleton<IUserInputService,UserInputService>();
                services.AddSingleton<MouseInput>();
                services.AddSingleton<KeyboardInput>();

                services.AddSingleton<NavigationStore>();
                services.AddSingleton<Func<HomeViewModel>>(s => s.GetRequiredService<HomeViewModel>);
                services.AddSingleton<INavigationService<HomeViewModel>, NavigationService<HomeViewModel>>();
                services.AddSingleton<Func<LoginViewModel>>(s=> s.GetRequiredService<LoginViewModel>);
                services.AddSingleton<INavigationService<LoginViewModel>, NavigationService<LoginViewModel>>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<PopupViewModel>();

                services.AddSingleton<SyncCommand>();
                services.AddSingleton<SyncViewModel>();


                services.AddTransient<LoginViewModel>();   
                services.AddTransient<HomeViewModel>();

                services.Configure<AppOption>(hostcontext.Configuration.GetSection(nameof(AppOption)));
                services.Configure<ServerOption>(hostcontext.Configuration.GetSection(nameof(ServerOption)));



                services.AddSingleton(s=> new PopupWindow
                {
                    DataContext = s.GetRequiredService<PopupViewModel>()
                });


                services.AddSingleton(s => new MainWindow
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                }); ;

            }).Build();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
            MessageBox.Show("Closing the application" + e.Message);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "IdealTimeTracker";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Application is already running");
                Application.Current.Shutdown();
            }
            else
            {
                NavigationStore _navigationStore = _host.Services.GetRequiredService<NavigationStore>();
                _navigationStore.CurrentViewModel = _host.Services.GetRequiredService<LoginViewModel>();
                _host.Start();
                UserContextDbFactory userContextDbFactory = _host.Services.GetRequiredService<UserContextDbFactory>();
                using (UserContext userContext = userContextDbFactory.CreateDbContext())
                {

                    userContext.Database.Migrate();
                }
                MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();
                
                base.OnStartup(e);


            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
