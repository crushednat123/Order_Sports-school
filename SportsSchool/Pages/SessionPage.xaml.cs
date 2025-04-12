using SportsSchool.Entities;
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
    /// Логика взаимодействия для SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page
    {
        public static int IdSession = 0;
        public SessionPage()
        {
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSerch.Text.Length != 0)
            {
                try
                {
                    listSession.ItemsSource = App.DataBase.Sessions.ToList().Where(p =>

                    (p.Coach.FullName.ToString().ToLower().Contains(tbSerch.Text.ToLower()) ||
                    p.Time.ToString().ToLower().Contains(tbSerch.Text.ToLower()) ||
                    p.Sports.Name.ToString().ToLower().Contains(tbSerch.Text.ToLower()))).ToList().OrderByDescending(p => p.Date);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void tbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSerch.Text.Length == 0)
            {
                listSession.ItemsSource = App.DataBase.Sessions.ToArray().OrderByDescending(p => p.Date);
            }
        }

        private void listSession_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (listSession.SelectedItem != null)
                {
                    
                    var selectedItem = (Sessions) listSession.SelectedItem;


                    GlobalVarbinary.StatysPage = 1;
                    NavigeteManager.StartFrame.Navigate(new EditSessionPage(selectedItem));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdSession_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.Navigate(new EditSessionPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(GlobalVarbinary.StatysPage == 1)
            {
                App.DataBase.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }

            if (GlobalVarbinary.StatysPage == 0)
            {
                listSession.ItemsSource = App.DataBase.Sessions.ToArray().OrderByDescending(p => p.Date); 
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mesBoxRes = MessageBox.Show("Вы точно хотите удалить это занятие? \n" +
                "Вся информация связанная с этим днём удалится!", "Внимание",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
            if (mesBoxRes == MessageBoxResult.Yes)
            {
                try
                {
                    IdSession = (listSession.SelectedItem as Sessions).Id;
                    App.DataBase.Sessions.Remove(listSession.SelectedItem as Sessions);
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    listSession.ItemsSource = App.DataBase.Sessions.ToArray().OrderByDescending(p => p.Date);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
