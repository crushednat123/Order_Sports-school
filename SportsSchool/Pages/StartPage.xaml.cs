using SportsSchool.Logic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportsSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void borderUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalVarbinary.CheckRoot = 0;
            NavigeteManager.StartFrame.Navigate(new SportsOverviewPage());
        }

        private void borderAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            borderAdmin.IsEnabled = false;
            borderUser.IsEnabled = false;
            gridAdmin.Visibility = Visibility.Visible;
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gridAdmin.Visibility = Visibility.Hidden;
            borderAdmin.IsEnabled = true;
            borderUser.IsEnabled = true;
        }

        private void btnRoot_Click(object sender, RoutedEventArgs e)
        {
            if(tbLogin.Text == "root" && tbPassword.Password == "root")
            {
                tbLogin.Text = "";
                tbPassword.Password = "";
                GlobalVarbinary.CheckRoot = 1;
                NavigeteManager.StartFrame.Navigate(new AdminPage());
            }
        }
    }
}
