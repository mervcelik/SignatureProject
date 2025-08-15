using Signify.WPF.UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YamlDotNet.Serialization;

namespace Signify.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomePage _homePage;
        public MainWindow(HomePage homePage)
        {
            InitializeComponent();
            _homePage = homePage;
            try
            {
                ListViewMenu.SelectedItem = ItemHome;

            }
            catch (Exception)
            {

                // throw;
            }
        }
        #region TopBar buttons Events
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeSizeWindow();
        }
        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            if (base.WindowState != WindowState.Minimized)
            {
                base.WindowState = WindowState.Minimized;
            }
        }

        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            ChangeSizeWindow();
        }

        private void ChangeSizeWindow()
        {
            var workArea = SystemParameters.WorkArea;
            if (Top == workArea.Top && Left == workArea.Left && Width == workArea.Width && Height == workArea.Height)
            {
                this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
                this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;

                this.Width = 1024;
                this.Height = 728;
            }
            else
            {
                this.Top = workArea.Top;
                this.Left = workArea.Left;
                this.Width = workArea.Width;
                this.Height = workArea.Height;

            }
        }
        #endregion


        #region NAVİGATİON MENÜ EVENTS

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            NavigationMenuColumn.Width = new GridLength(200);
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            NavigationMenuColumn.Width = new GridLength(70);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = _homePage;
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
