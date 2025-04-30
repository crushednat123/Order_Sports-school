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
using static SportsSchool.Pages.StydentPage;

namespace SportsSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();

         
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dateUsers.SelectedItem != null)
            {
                var selectedItem = (Users) dateUsers.SelectedItem;              
               
                GlobalVarbinary.StatysPage = 1;
                GlobalVarbinary.CheckRoot = 1;
                NavigeteManager.StartFrame.Navigate(new AdUesrPage(selectedItem));                
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (GlobalVarbinary.StatysPage == 1)
            {
                // Обновляем трекер изменений
                App.DataBase.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                RefreshDataGrid();
            }

            if (GlobalVarbinary.StatysPage == 0)
            {
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            dateUsers.ItemsSource = App.DataBase.Users.Where(p => p.Login != GlobalVarbinary.Id).ToList();
        }

        private void btnAdUsers_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            GlobalVarbinary.CheckRoot = 1;
            NavigeteManager.StartFrame.Navigate(new AdUesrPage(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить пользователя?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var selectedItem = (Users) dateUsers.SelectedItem;
                var userDelete = App.DataBase.Users.Find(selectedItem.Login);
                if (userDelete == null) return;

                App.DataBase.Users.Remove(userDelete);
                App.DataBase.SaveChanges();

                MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
    }
}
