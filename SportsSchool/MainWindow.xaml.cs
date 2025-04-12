using SportsSchool.Logic;
using SportsSchool.Pages;
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

namespace SportsSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigeteManager.StartFrame =  startFrame;

            if(GlobalVarbinary.CheckRoot == 0)
            {
               startFrame.Navigate(new StartPage());
            }

            if (GlobalVarbinary.CheckRoot == 1)
            {
                startFrame.Navigate(new AdminPage());
            }

        }

        private void startFrame_ContentRendered(object sender, EventArgs e)
        {
            if (startFrame.Content.GetType().Name == "StartPage")
            {
               btnBack.Visibility = Visibility.Hidden;
               btnHome.Visibility = Visibility.Hidden;
            }

            if (startFrame.Content.GetType().Name == "AdminPage")
            {
                
                btnBack.Visibility = Visibility.Visible;
                btnHome.Visibility = Visibility.Visible;
            }

            if (startFrame.Content.GetType().Name == "SportsOverviewPage")
            {

                btnBack.Visibility = Visibility.Visible;
                btnHome.Visibility = Visibility.Hidden;
            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.GoBack();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVarbinary.CheckRoot == 0)
            {
                startFrame.Navigate(new StartPage());
            }

            if (GlobalVarbinary.CheckRoot == 1)
            {
                startFrame.Navigate(new AdminPage());
            }
        }      
    }
}
