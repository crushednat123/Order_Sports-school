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
    /// Логика взаимодействия для ParentsPage.xaml
    /// </summary>
    public partial class ParentsPage : Page
    {
        public ParentsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Редактировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateParents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dateParents.SelectedItem != null)
            {
                var selectedItem = (Parents) dateParents.SelectedItem;

                GlobalVarbinary.StatysPage = 1;              
                NavigeteManager.StartFrame.Navigate(new EditParentsPage(selectedItem));
            }
        }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdParents_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.Navigate(new EditParentsPage(null));
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
            dateParents.ItemsSource = App.DataBase.Parents.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить родителя?", "Внимание",
              MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var selectedItem = (Parents) dateParents.SelectedItem;
                var parentsDelete = App.DataBase.Parents.Find(selectedItem.Id);
                if (parentsDelete == null) return;

                App.DataBase.Parents.Remove(parentsDelete);
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
