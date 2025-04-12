using MaterialDesignThemes.Wpf;
using SportsSchool.Entities;
using SportsSchool.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace SportsSchool.Pages.AddEdditPage
{
    /// <summary>
    /// Логика взаимодействия для EditSportPage.xaml
    /// </summary>
    public partial class EditSportPage : Page
    {
        private Sports _curentSport = new Sports();
        public EditSportPage(Sports selectsport)
        {
            InitializeComponent();

            if (selectsport != null)
            {
                _curentSport = selectsport;
            }

            DataContext = _curentSport;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFormValid())
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }

            string sql = "";
            try
            {
                if (GlobalVarbinary.StatysPage == 0) // Режим добавления
                {
                    sql = $"INSERT INTO Sports (Name, Price) VALUES ('{NameTextBox.Text}', " +
                          $"{tbRyb.Text})";
                }
                else if (GlobalVarbinary.StatysPage == 1) // Режим редактирования
                {
                    sql = $"UPDATE Sports SET Name = '{NameTextBox.Text}', " +
                          $"Price = {tbRyb.Text}" +
                          $"WHERE Id = {_curentSport.Id}";
                }

                App.DataBase.Database.ExecuteSqlCommand(sql);
                MessageBox.Show("Данные сохранены");
                NavigeteManager.StartFrame.GoBack();
            }
           
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(tbRyb.Text);
        }

        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
