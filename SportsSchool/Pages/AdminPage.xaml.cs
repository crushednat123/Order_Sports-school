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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void btnSessionPage_Click(object sender, RoutedEventArgs e)
        {
            NavigeteManager.StartFrame.Navigate(new SessionPage());
        }

        private void btnChoach_Click(object sender, RoutedEventArgs e)
        {
          
            NavigeteManager.StartFrame.Navigate(new ChoahPage());
        }

        private void btnStydent_Click(object sender, RoutedEventArgs e)
        {
            NavigeteManager.StartFrame.Navigate(new StydentPage());
        }

        private void btnSport_Click(object sender, RoutedEventArgs e)
        {
           
            NavigeteManager.StartFrame.Navigate(new SportsOverviewPage());
        }
    }
}
