using ApiService.Services;
using Core.Security.Entities;
using Core.Security.Enums;
using MaterialDesignThemes.Wpf;
using Signify.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Signify.WPF.Forms.Shared
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AuthApiService _authApiService;
        MainWindow _mainWindow;
        VerifyEmailWindow _verifyEmailWindow;
        public LoginWindow(AuthApiService authApiService, MainWindow mainWindow)
        {
            InitializeComponent();
            _authApiService = authApiService;
            _mainWindow = mainWindow;
            logoImg.Source = Properties.Resources.logo.ToImageSource();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = txtEmail.Text;
                var password = Password.Password;

                var login = RNH.GetOrThrow(await _authApiService.LoginAsync(new Application.Features.Auth.Commands.Login.LoginCommand
                {
                    UserForLoginDto = new Core.Application.Dtos.UserForLoginDto { Email = email, Password = password },
                    IpAddress = IpHelper.GetLocalIPv4()
                }));
                if (login.AccessToken == null)
                {
                    _verifyEmailWindow = new VerifyEmailWindow();
                    _verifyEmailWindow.ShowDialog();
                    var login2 = RNH.GetOrThrow(await _authApiService.LoginAsync(new Application.Features.Auth.Commands.Login.LoginCommand
                    {
                        UserForLoginDto = new Core.Application.Dtos.UserForLoginDto { Email = email, Password = password, AuthenticatorCode = _verifyEmailWindow.AuthenticatorCode },
                        IpAddress = IpHelper.GetLocalIPv4()
                    }));
                    if (login2.AccessToken != null)
                    {
                        App.AccessToken = login2.AccessToken.Token;
                        _mainWindow.Show();
                        this.Close();
                    }
                }
                else if (login.AccessToken != null)
                {
                    App.AccessToken = login.AccessToken.Token;
                    _mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

        }

        private void TxtForgetPasword_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
