using SportsSchool.Entities;
using SportsSchool.Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace SportsSchool.Pages.AddEdditPage
{
    /// <summary>
    /// Логика взаимодействия для EditStydentPage.xaml
    /// </summary>
    public partial class EditStydentPage : Page
    {
        private Student _student;

        public EditStydentPage(int idStud)
        {
            InitializeComponent();

            if(idStud != 0)
            {
                LoadCoachData(idStud);
            }
            else
            {
                // Заполнение видами спорта
                var sportsFromDb = App.DataBase.Sports.ToList();
                var sportsViewModels = sportsFromDb.Select(s => new SportViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsSelected = false // или другое значение по умолчанию
                }).ToList();

                chekSport.ItemsSource = sportsViewModels;
            }

            cbParent.ItemsSource = App.DataBase.Parents.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool chekDate = IsFormValid();           
            if (chekDate)
            {
                // Добавление
                if (GlobalVarbinary.StatysPage == 0)
                {
                    try
                    {
                        // Получаем выбранного родителя (если есть)
                        int? selectedParentId = (cbParent.SelectedItem as Parents)?.Id;

                        string sql = "INSERT INTO Student (Name, SurName, Patronymic, BirthDate, IdParents) " +
                                    "VALUES (@Name, @Surname, @Patronymic, @BirthDate, @IdParents)";

                        var parameters = new SqlParameter[]
                                            {
                        new SqlParameter("@Name", NameTextBox.Text),
                        new SqlParameter("@Surname", SurnameTextBox.Text),
                        new SqlParameter("@Patronymic", PatronymicTextBox.Text),
                        new SqlParameter("@BirthDate", DateTextBox.Text),
                        new SqlParameter("@IdParents", selectedParentId ?? (object)DBNull.Value)
                        };

                        App.DataBase.Database.ExecuteSqlCommand(sql, parameters);

                        // Получаем выбранные виды спорта
                        var sportsViewModels = chekSport.ItemsSource.Cast<SportViewModel>().ToList();
                        var selectedSports = sportsViewModels.Where(s => s.IsSelected).ToList();
                     
                       

                        // Получаем ID добавленного студента
                        var newStudId = App.DataBase.Student.Max(c => c.Id); // Получение последнего добавленного ID

                        // Сохраняем связи в SportsStudents
                        foreach (var sport in selectedSports)
                        {
                            var insertSportsSql = $"INSERT INTO SportsStudents (IdStudent, IdSport) VALUES ({newStudId}, {sport.Id});";
                            App.DataBase.Database.ExecuteSqlCommand(insertSportsSql);
                        }

                        MessageBox.Show("Студент добавлен");

                        NavigeteManager.StartFrame.GoBack();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                // Редактирование          
                if (GlobalVarbinary.StatysPage == 1) 
                {
                    try
                    {                        
                        int? selectedParentId = (cbParent.SelectedItem as Parents)?.Id;
               
                        string updateStudentSql = @"UPDATE Student SET 
                                Name = @Name, 
                                SurName = @Surname, 
                                Patronymic = @Patronymic, 
                                BirthDate = @BirthDate, 
                                IdParents = @IdParents 
                                WHERE Id = @StudentId";

                        var parameters = new SqlParameter[]
                        {
                                new SqlParameter("@Name", NameTextBox.Text),
                                new SqlParameter("@Surname", SurnameTextBox.Text),
                                new SqlParameter("@Patronymic", PatronymicTextBox.Text),
                                new SqlParameter("@BirthDate", DateTextBox.Text),
                                new SqlParameter("@IdParents", selectedParentId ?? (object)DBNull.Value),
                                new SqlParameter("@StudentId", _student.Id) 
                        };

                        // Обновляем данные студента
                        App.DataBase.Database.ExecuteSqlCommand(updateStudentSql, parameters);

                        // Получаем выбранные виды спорта
                        var sportsViewModels = chekSport.ItemsSource.Cast<SportViewModel>().ToList();
                        var selectedSports = sportsViewModels.Where(s => s.IsSelected).ToList();

                        // Удаляем все текущие связи студента с видами спорта
                        string deleteSportsSql = "DELETE FROM SportsStudents WHERE IdStudent = @StudentId";
                        App.DataBase.Database.ExecuteSqlCommand(deleteSportsSql, new SqlParameter("@StudentId", _student.Id));

                        // Добавляем новые связи
                        foreach (var sport in selectedSports)
                        {
                            string insertSportsSql = "INSERT INTO SportsStudents (IdStudent, IdSport) VALUES (@StudentId, @SportId)";
                            var sportParams = new SqlParameter[]
                            {
                                    new SqlParameter("@StudentId", _student.Id),
                                    new SqlParameter("@SportId", sport.Id)
                            };
                            App.DataBase.Database.ExecuteSqlCommand(insertSportsSql, sportParams);
                        }

                        MessageBox.Show("Данные студента обновлены");
                        NavigeteManager.StartFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении данных студента: {ex.Message}");
                    }
                }

            }
            else
            {
                MessageBox.Show("Не все данные заполнены");
            }
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                 string.IsNullOrWhiteSpace(SurnameTextBox.Text) ||
                 string.IsNullOrWhiteSpace(PatronymicTextBox.Text) ||
                 DateTextBox.SelectedDate == null)
            {
                return false; // Если хоть одно поле пустое, форма недействительна
            }
            return true; // Все поля заполнены
        }

        /// <summary>
        /// Заполнение данными 
        /// </summary>
        /// <param name="idStud">айди студента</param>
        /// <exception cref="NotImplementedException"></exception>
        private void LoadCoachData(int idStud)
        {
            try
            {
                _student = App.DataBase.Student.FirstOrDefault(p => p.Id == idStud);

                DataContext = _student;

                // Получаем все виды спорта
                var sports = App.DataBase.Sports.ToArray();

                // Получаем ID выбранных видов спорта для студента
                var selectedSportsIds = App.DataBase.SportsStudents
                                                .Where(sc => sc.IdStudent == idStud)
                                                .Select(sc => sc.IdSport)
                                                .ToArray();

                // Создаем список для отображения
                var sportsViewModels = sports.Select(s => new SportViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsSelected = selectedSportsIds.Contains(s.Id)
                }).ToList();

                // Устанавливаем ItemsSource для вашего ItemsControl
                chekSport.ItemsSource = sportsViewModels;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных тренера: {ex.Message}");
            }
        }

        /// <summary>
        /// Запрет ввода с клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
