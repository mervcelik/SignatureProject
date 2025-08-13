using ApiService.Registration;
using ApiService.Services;
using Core.Security.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Signify.WPF.Extensions;
using Signify.WPF.Windows;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace Signify.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static string? AccessToken { get; set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                ServiceProvider = serviceCollection.BuildServiceProvider();

                AccessToken = await LoadAccessToken();

                if (string.IsNullOrEmpty(AccessToken))
                {
                    var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
                    loginWindow.Show();
                }
                else
                {
                    var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                    mainWindow.Show();
                }
            }
            catch (Exception)
            {

               // throw;
            }
        }

        private async Task<string?> LoadAccessToken()
        {
            try
            {
                var token = Signify.WPF.Properties.Settings.Default.Token;
                var accessTokenExpiration = token.IsExpired();
                if (accessTokenExpiration)
                {
                    var authApiService = ServiceProvider.GetRequiredService<AuthApiService>();
                    var newToken = RNH.GetOrThrow(await authApiService.RefreshTokenAsync());
                    return newToken != null ? newToken : null;
                }
                return token;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<VerifyEmailWindow>();

            services.AddApiServices();
        }
    }
}