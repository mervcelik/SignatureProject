using ApiService.Registration;
using Microsoft.Extensions.DependencyInjection;
using Signify.WPF.Forms.Shared;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Signify.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static string AccessToken { get; set; }
        public static string RefreshToken { get; set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AccessToken = LoadAccessToken();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

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

        private string LoadAccessToken()
        {
            return null;
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