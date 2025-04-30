using SportsSchool.Logic;
using SportsSchool.Pages.AddEdditPage;
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

        private void btnVoiti_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text.Length == 0 || tbPassword.Password.Length == 0)
            {
                MessageBox.Show("Вы не ввели логин или пароль", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                try
                {
                    var user = App.DataBase.Users.Where(p =>
                    p.Login == tbLogin.Text && p.Password == tbPassword.Password).FirstOrDefault();

                    if (user != null)
                    {
                        GlobalVarbinary.Id = user.Login;
                        if (user.IdRole == 1)
                        {
                            GlobalVarbinary.CheckRoot = 1;
                            NavigeteManager.StartFrame.Navigate(new AdminPage());
                            MessageBox.Show("Вы вошли как администратор", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        if (user.IdRole == 2)
                        {
                            GlobalVarbinary.CheckRoot = 0;
                            NavigeteManager.StartFrame.Navigate(new SportsOverviewPage());

                            MessageBox.Show("Вы вошли как гость", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Нет подключения к серверу", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.CheckRoot = 0;
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.Navigate(new AdUesrPage(null));
        }
    }
}
