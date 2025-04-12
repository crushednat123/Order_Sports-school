using SportsSchool.Entities;
using SportsSchool.Logic;
using System;

using System.Linq;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace SportsSchool.Pages.AddEdditPage
{
    /// <summary>
    /// Логика взаимодействия для EditSessionPage.xaml
    /// </summary>
    public partial class EditSessionPage : Page
    {
        public static int idSport { get; set; }
        public static int idCoach { get; set; }

        private Sessions _currentSession = new Sessions();
        public EditSessionPage(Sessions selectSession)
        {
            InitializeComponent();

            if (selectSession != null)
            {
                _currentSession = selectSession;
            }
            DataContext = _currentSession;

            cbSportName.ItemsSource = App.DataBase.Sports.ToList();
            cbCoacheFullName.ItemsSource = App.DataBase.Coach.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool chekDate = IsFormValid();
            string sql = "";
            if (chekDate)
            {
                // Добавление
                if (GlobalVarbinary.StatysPage == 0)
                {
                    sql = $"INSERT INTO Sessions (IdSport, Date, Time, Location, IdCoach) " +
                    $"VALUES ({idSport}, '{DateTextBox.Text}', '{TimePiker.Text}', '{LocationTextBox.Text}', {idCoach});";

                }
                // Редактирование
                if (GlobalVarbinary.StatysPage == 1)
                {
                    sql = $"UPDATE Sessions SET IdSport = {idSport}, Date = '{DateTextBox.Text}', Time = '{TimePiker.Text}'," +
                       $" Location = '{LocationTextBox.Text}', IdCoach = {idCoach}" +
                       $" WHERE Id = {_currentSession.Id};";
                }

                try
                {
                    App.DataBase.Database.ExecuteSqlCommand(sql);
                    MessageBox.Show("Данные сохранены");
                    NavigeteManager.StartFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Не все данные заполнены");
            }
        }


        /// <summary>
        /// Проверка на пустоту
        /// </summary>
        /// <returns></returns>
        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(cbSportName.Text) ||
                string.IsNullOrWhiteSpace(cbCoacheFullName.Text) ||
                DateTextBox.SelectedDate == null ||  // Проверка выбранной даты
                string.IsNullOrWhiteSpace(TimePiker.Text) ||
                string.IsNullOrWhiteSpace(LocationTextBox.Text))
            {
                return false; // Если хоть один элемент не заполнен, форма недействительна
            }
            return true; // Все элементы заполнены
        }

        private void cbCoacheFullName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCoacheFullName.SelectedItem != null)
            {
                var selectedCoach = (Coach) cbCoacheFullName.SelectedItem;
                idCoach = selectedCoach.Id;
            }
        }

        private void cbSportName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSportName.SelectedItem != null)
            {
                var selectedSport = (Sports) cbSportName.SelectedItem;
                idSport = selectedSport.Id;
            }
        }

        /// <summary>
        /// Перехват ввода с клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimePiker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// Перехват ввода с клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
