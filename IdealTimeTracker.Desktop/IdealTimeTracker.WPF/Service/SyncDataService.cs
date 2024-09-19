using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Repository.Interface;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Utility.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace IdealTimeTracker.WPF.Service
{
    public class SyncDataService : ISyncDataService
    {
        private readonly IUserLogRepo _userLogRepo;
        private readonly IUserActivityRepo _userActivityRepo;
        private readonly IOptions<ServerOption> _options;
        private readonly IUserRepo _userRepo;
        private readonly IApplicationConfigRepo _applicationConfigRepo;

        private readonly UserStore _userStore;
        public SyncDataService(IUserLogRepo userLogRepo, UserStore userStore,IUserActivityRepo userActivityRepo, IUserRepo userRepo ,IApplicationConfigRepo applicationConfigRepo, IOptions<ServerOption> options)
        {
            _options = options;
            _userLogRepo = userLogRepo;
            _userStore = userStore;
            _userActivityRepo = userActivityRepo;
            _userRepo = userRepo;
            _applicationConfigRepo = applicationConfigRepo;
        }
        public async Task<bool> isInternetAvaiable()
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync("8.8.8.8", 3000);
                return reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  async Task SyncData()
        {
                var userLogs = _userLogRepo.GetAllLog(_userStore.Date.Date);


                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{_options.Value.BaseUrl}UserActivity");
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var userActivities = await response.Content.ReadFromJsonAsync<List<UserActivity>>();
                        if (userActivities != null)
                        {
                            _userActivityRepo.Merge(userActivities);

                        }
                    }
                }

                using (HttpClient client = new HttpClient())
                {
                    var content = JsonSerializer.Serialize(userLogs);

                    var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.Value.BaseUrl}UserLog/Logs");
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        _userLogRepo.Delete(userLogs);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }

                }

            using (HttpClient client = new HttpClient())
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"{_options.Value.BaseUrl}User/Employee");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadFromJsonAsync<List<User>>();
                    if (users != null)
                    {
                        _userRepo.Merge(users);
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

            }

            using (HttpClient client = new HttpClient())
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"{_options.Value.BaseUrl}ApplicationConfig/Config");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var applicationConfigurations = await response.Content.ReadFromJsonAsync<List<ApplicationConfiguration>>();
                    if (applicationConfigurations != null)
                    {
                        _applicationConfigRepo.Merge(applicationConfigurations);
                        var config = _applicationConfigRepo.GetAll();
                        _userStore.setAdminConfig(config);
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

            }


        }
    }
}
