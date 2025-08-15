using ApiService.Base;
using ApiService.Registration;
using ApiService.Services;
using Core.Security.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Signify.WPF.Extensions;
using Signify.WPF.Handler;
using Signify.WPF.UserControls;
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
        public static string? Token { get; set; }
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
            catch (Exception ex)
            {

                // throw;
            }
        }

        private async Task<string?> LoadAccessToken()
        {
            try
            {
                var accessToken = Signify.WPF.Properties.Settings.Default.AccessToken;
                var refreshToken = Signify.WPF.Properties.Settings.Default.RefreshToken;
                Token = refreshToken;
                var accessTokenExpiration = accessToken.IsExpired();
                if (accessTokenExpiration)
                {
                    var authApiService = ServiceProvider.GetRequiredService<AuthApiService>();
                    var newToken = RNH.GetOrThrow(await authApiService.ReLoginAsync(new Application.Features.Auth.Commands.ReLogin.ReLoginCommand
                    {
                        Token = Token,
                    }));
                    return newToken != null && newToken.Token != null ? newToken.AccessToken : null;
                }
                return accessToken;
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
            services.AddSingleton<HomePage>();


            services.AddHttpClient<ApiServiceFactory>("AuthorizedClient")
    .AddHttpMessageHandler<AuthorizationHandler>();
            services.AddApiServices();
        }


        public static T GetService<T>() where T : class
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}