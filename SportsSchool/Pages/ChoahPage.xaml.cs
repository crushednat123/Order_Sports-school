using SportsSchool.Logic;
using SportsSchool.Pages.AddEdditPage;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChoahPage.xaml
    /// </summary>
    public partial class ChoahPage : Page
    {
        public ChoahPage()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSerch.Text.Length != 0)
            {
                try
                {
                    var searchText = tbSerch.Text.ToLower();

                    var groupedData = App.DataBase.SportsAndCoach
                        .AsEnumerable()
                        .Where(p => p.Coach.SurName.ToLower().Contains(searchText) ||
                                   p.Coach.Name.ToLower().Contains(searchText) ||
                                   p.Coach.Patronymic.ToLower().Contains(searchText) ||
                                   p.Sports.Name.ToLower().Contains(searchText))
                        .GroupBy(sc => new
                        {
                            sc.Coach.SurName,
                            sc.Coach.Name,
                            sc.Coach.Patronymic,
                            sc.Coach.Telephone
                        })
                        .Select(g => new CoachGroupViewModel
                        {
                            SurName = g.Key.SurName,
                            Name = g.Key.Name,
                            Patronymic = g.Key.Patronymic,
                            Telephone = g.Key.Telephone,
                            Sports = string.Join(", ", g.Select(x => x.Sports.Name))
                        })
                        .ToList();

                    dateCoach.ItemsSource = groupedData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void tbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSerch.Text.Length == 0)
            {
                try
                {
                    DateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        // Добавление
        private void btnAdChoah_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.Navigate(new EditCoachPage(0));
        }

        // Удаление
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dateCoach.SelectedItem == null) return;

            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить тренера?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Получаем выбранную ViewModel
                    var selectedCoach = (CoachGroupViewModel) dateCoach.SelectedItem;

                    // Находим тренера в базе по ID
                    var coachToDelete = App.DataBase.Coach.Find(selectedCoach.Id);

                    if (coachToDelete != null)
                    {
                        App.DataBase.Coach.Remove(coachToDelete);
                        App.DataBase.SaveChanges();
                        MessageBox.Show("Данные удалены!", "",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        DateGrid(); // Обновляем таблицу
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateCoach_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dateCoach.SelectedItem != null)
                {
                    var selectedItem = (CoachGroupViewModel) dateCoach.SelectedItem;

                    // Получаем оригинальную запись тренера из базы данных
                    var coach = App.DataBase.Coach.FirstOrDefault(c =>
                        c.SurName == selectedItem.SurName &&
                        c.Name == selectedItem.Name &&
                        c.Patronymic == selectedItem.Patronymic &&
                        c.Telephone == selectedItem.Telephone);

                    if (coach != null)
                    {
                        GlobalVarbinary.StatysPage = 1;

                        NavigeteManager.StartFrame.Navigate(new EditCoachPage(coach.Id));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (GlobalVarbinary.StatysPage == 1)
            {
                // Обновляем трекер изменений
                App.DataBase.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                // Явно перегружаем данные с группировкой
                var groupedData = App.DataBase.SportsAndCoach
                                .AsEnumerable()
                                .GroupBy(sc => new
                                {
                                    sc.Coach.SurName,
                                    sc.Coach.Name,
                                    sc.Coach.Patronymic,
                                    sc.Coach.Telephone
                                })
                                .Select(g => new CoachGroupViewModel
                                {
                                    SurName = g.Key.SurName,
                                    Name = g.Key.Name,
                                    Patronymic = g.Key.Patronymic,
                                    Telephone = g.Key.Telephone,
                                    Sports = string.Join(", ", g.Select(x => x.Sports.Name))
                                })
                                .ToList();

                dateCoach.ItemsSource = groupedData;
            }

            if (GlobalVarbinary.StatysPage == 0)
            {
                DateGrid();
            }
        }

        private void DateGrid()
        {
            var groupedData = App.DataBase.SportsAndCoach
                            .AsEnumerable()
                            .GroupBy(sc => new
                            {
                                sc.Coach.Id,  // Добавляем ID в группировку
                                sc.Coach.SurName,
                                sc.Coach.Name,
                                sc.Coach.Patronymic,
                                sc.Coach.Telephone
                            })
                            .Select(g => new CoachGroupViewModel
                            {
                                Id = g.Key.Id,  // Заполняем ID
                                SurName = g.Key.SurName,
                                Name = g.Key.Name,
                                Patronymic = g.Key.Patronymic,
                                Telephone = g.Key.Telephone,
                                Sports = string.Join(", ", g.Select(x => x.Sports.Name))
                            })
                            .ToList();
            dateCoach.ItemsSource = groupedData;
        }
    }
}
