using MaterialDesignThemes.Wpf;
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

namespace SportsSchool.Pages.AddEdditPage
{
    /// <summary>
    /// Логика взаимодействия для EditCoachPage.xaml
    /// </summary>
    public partial class EditCoachPage : Page
    {
       

        private Coach _currentCoach;
        public EditCoachPage(int coachId)
        {
            InitializeComponent();

            if (coachId != 0)
            {
                LoadCoachData(coachId);
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
        }

        private void LoadCoachData(int coachId)
        {
            try
            {
                _currentCoach = App.DataBase.Coach.FirstOrDefault(p => p.Id == coachId);

                if (_currentCoach != null)
                {
                    DataContext = _currentCoach;
                    // Получаем все виды спорта
                    var sports = App.DataBase.Sports.ToArray();

                    // Получаем ID выбранных видов спорта для тренера
                    var selectedSportsIds = App.DataBase.SportsAndCoach
                                                    .Where(sc => sc.IdCoach == coachId)
                                                    .Select(sc => sc.IdSports)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных тренера: {ex.Message}");
            }
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
                    try
                    {
                        // SQL запрос для добавления тренера
                        sql = "INSERT INTO Coach (Name, SurName, Patronymic, Telephone) " +
                          $"VALUES ('{NameTextBox.Text}', '{SurnameTextBox.Text}', '{PatronymicTextBox.Text}', '{TelephoneTextBox.Text}');";

                        // Получаем выбранные виды спорта
                        var sportsViewModels = chekSport.ItemsSource.Cast<SportViewModel>().ToList();
                        var selectedSports = sportsViewModels.Where(s => s.IsSelected).ToList();


                        // Выполняем запрос на добавление тренера
                        App.DataBase.Database.ExecuteSqlCommand(sql);

                        // Получаем ID добавленного тренера
                        var newCoachId = App.DataBase.Coach.Max(c => c.Id); // Получение последнего добавленного ID



                        // Сохраняем связи в SportsAndCoach
                        foreach (var sport in selectedSports)
                        {
                            var insertSportsSql = $"INSERT INTO SportsAndCoach (IdCoach, IdSports) VALUES ({newCoachId}, {sport.Id});";
                            App.DataBase.Database.ExecuteSqlCommand(insertSportsSql);
                        }

                        MessageBox.Show("Тренер добавлен");

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
                        // Обновляем данные тренера
                        sql = $"UPDATE Coach SET Name = '{NameTextBox.Text}', SurName = '{SurnameTextBox.Text}', " +
                              $"Patronymic = '{PatronymicTextBox.Text}', Telephone = '{TelephoneTextBox.Text}' " +
                              $"WHERE Id = {_currentCoach.Id};";

                        App.DataBase.Database.ExecuteSqlCommand(sql);

                        // Получаем текущие выбранные виды спорта из чекбоксов
                        var sportsViewModels = chekSport.ItemsSource.Cast<SportViewModel>().ToList();
                        var selectedSports = sportsViewModels.Where(s => s.IsSelected).ToList();

                        // Получаем текущие связи тренера с видами спорта из БД
                        var currentSportsInDb = App.DataBase.SportsAndCoach
                                                        .Where(sc => sc.IdCoach == _currentCoach.Id)
                                                        .ToList();

                        // Определяем какие связи нужно добавить, а какие удалить
                        var sportsToAdd = selectedSports
                            .Where(s => !currentSportsInDb.Any(db => db.IdSports == s.Id))
                            .ToList();

                        var sportsToRemove = currentSportsInDb
                            .Where(db => !selectedSports.Any(s => s.Id == db.IdSports))
                            .ToList();

                        // Добавляем новые связи
                        foreach (var sport in sportsToAdd)
                        {
                            var insertSql = $"INSERT INTO SportsAndCoach (IdCoach, IdSports) VALUES ({_currentCoach.Id}, {sport.Id});";
                            App.DataBase.Database.ExecuteSqlCommand(insertSql);
                        }

                        // Удаляем старые связи
                        foreach (var sport in sportsToRemove)
                        {
                            var deleteSql = $"DELETE FROM SportsAndCoach WHERE IdCoach = {_currentCoach.Id} AND IdSports = {sport.IdSports};";
                            App.DataBase.Database.ExecuteSqlCommand(deleteSql);
                        }

                        MessageBox.Show("Данные тренера обновлены");
                        NavigeteManager.StartFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
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
                string.IsNullOrWhiteSpace(TelephoneTextBox.Text))
            {
                return false; // Если хоть одно поле пустое, форма недействительна
            }
            return true; // Все поля заполнены
        }

        private void TelephoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            var regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
