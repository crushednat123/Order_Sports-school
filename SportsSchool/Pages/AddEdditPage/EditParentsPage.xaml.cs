using SportsSchool.Entities;
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
using System.Xml.Linq;

namespace SportsSchool.Pages.AddEdditPage
{
    /// <summary>
    /// Логика взаимодействия для EditParentsPage.xaml
    /// </summary>
    public partial class EditParentsPage : Page
    {
        private Parents _currentParents;
        public EditParentsPage(Parents selectParents)
        {
            InitializeComponent();

            if (selectParents != null)
            {
                _currentParents = selectParents;
            }
            DataContext = _currentParents;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isEmpty = FormValidator.AreFieldsEmpty(tbAddress, tbFirstName, tbLastName, tbMiddleName, tbPhone);

            if (isEmpty)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (GlobalVarbinary.StatysPage == 1)
            {
                try
                {
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Данные отредактированы", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigeteManager.StartFrame.GoBack();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (GlobalVarbinary.StatysPage == 0)
            {
                try
                {
                    Parents currentParents = new Parents();
                    currentParents.SurName = tbLastName.Text;
                    currentParents.Name = tbFirstName.Text;
                    currentParents.Patronymic = tbMiddleName.Text;
                    currentParents.Address = tbAddress.Text;
                    currentParents.Telephone = tbPhone.Text;


                    App.DataBase.Parents.Add(currentParents);
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Данные добавлены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigeteManager.StartFrame.GoBack();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
