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

namespace Signify.WPF.Forms.Shared
{
    /// <summary>
    /// Interaction logic for VerifyEmailWindow.xaml
    /// </summary>
    public partial class VerifyEmailWindow : Window
    {
        public  string AuthenticatorCode { get; set; }
        public VerifyEmailWindow()
        {
            InitializeComponent();
        }

        private void SingUpButton_Click(object sender, RoutedEventArgs e)
        {
            AuthenticatorCode = VerifyEmailTextBox.Text;
            if (string.IsNullOrWhiteSpace(AuthenticatorCode))
            {
                MessageBox.Show("Please enter the verification code sent to your email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Close();
        }
    }
}
