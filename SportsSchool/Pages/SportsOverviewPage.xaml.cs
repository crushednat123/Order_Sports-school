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
    /// Логика взаимодействия для SportsOverviewPage.xaml
    /// </summary>
    public partial class SportsOverviewPage : Page
    {
        public SportsOverviewPage()
        {
            InitializeComponent();

            if (GlobalVarbinary.CheckRoot == 0)
            {
                gridAdmin.Visibility = Visibility.Hidden;
            }

            if (GlobalVarbinary.CheckRoot == 1)
            {
                gridAdmin.Visibility = Visibility.Visible;
            }

        }

        private void GridDate()
        {
            dateSport.ItemsSource = App.DataBase.Sports.ToArray();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mesBoxRes = MessageBox.Show("Вы точно хотите удалить этот спорт?", "Внимание",
              MessageBoxButton.YesNo,
              MessageBoxImage.Question);
            if (mesBoxRes == MessageBoxResult.Yes)
            {
                try
                {
                    int  IdDelete = (dateSport.SelectedItem as Sports).Id;
                    App.DataBase.Sports.Remove(dateSport.SelectedItem as Sports);
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    GridDate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAdSport_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.Navigate(new EditSportPage(null));
        }

        private void dateSport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(GlobalVarbinary.CheckRoot == 1)
            {
                GlobalVarbinary.StatysPage = 1;
                var row = ItemsControl.ContainerFromElement((DataGrid) sender, e.OriginalSource as DependencyObject) as DataGridRow;

                var sport = row.Item as Sports;

                NavigeteManager.StartFrame.Navigate(new EditSportPage(sport));
                    
                
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (GlobalVarbinary.StatysPage == 1)
            {
                App.DataBase.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }

            if (GlobalVarbinary.StatysPage == 0)
            {
                GridDate();
            } 
        }
    }
}
