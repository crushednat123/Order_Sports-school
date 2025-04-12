using SportsSchool.Logic;
using SportsSchool.Pages.AddEdditPage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для StydentPage.xaml
    /// </summary>
    public partial class StydentPage : Page
    {
        public class StudentGroup
        {
            public int Id { get; set; }
            public string StudentSurName { get; set; }
            public string StudentName { get; set; }
            public string StudentPatronymic { get; set; }
            public DateTime BirthDate { get; set; }
            public string ParentFullName { get; set; }
            public List<string> Sports { get; set; } = new List<string>();
            public bool IsExpanded { get; set; } = false;

          
            public string SportsDisplay => string.Join(", ", Sports);
        }

        public StydentPage()
        {
            InitializeComponent();           
        }

        private void tbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSerch.Text.Length == 0)
            {
                RefreshDataGrid();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSerch.Text.Length != 0)
            {
                try
                {
                    string searchText = tbSerch.Text.ToLower();

                    var query = from s in App.DataBase.Student
                                join ss in App.DataBase.SportsStudents on s.Id equals ss.IdStudent into ssGroup
                                from ss in ssGroup.DefaultIfEmpty()
                                join sp in App.DataBase.Sports on ss.IdSport equals sp.Id into spGroup
                                from sp in spGroup.DefaultIfEmpty()
                                join p in App.DataBase.Parents on s.IdParents equals p.Id into pGroup
                                from p in pGroup.DefaultIfEmpty()
                                select new
                                {
                                    Id = s.Id,
                                    StudentSurName = s.SurName,
                                    StudentName = s.Name,
                                    StudentPatronymic = s.Patronymic,
                                    BirthDate = s.BirthDate,
                                    SportName = sp != null ? sp.Name : null,
                                    ParentSurName = p != null ? p.SurName : null,
                                    ParentName = p != null ? p.Name : null,
                                    ParentPatronymic = p != null ? p.Patronymic : null
                                };

                    // Фильтрация с обработкой null-значений
                    var filteredResult = query.ToList()
                        .Where(r =>
                            (r.StudentSurName != null && r.StudentSurName.ToLower().Contains(searchText)) ||
                            (r.StudentName != null && r.StudentName.ToLower().Contains(searchText)) ||
                            (r.StudentPatronymic != null && r.StudentPatronymic.ToLower().Contains(searchText)) ||
                            (r.SportName != null && r.SportName.ToLower().Contains(searchText)) ||
                            (r.ParentSurName != null && r.ParentSurName.ToLower().Contains(searchText)) ||
                            (r.ParentName != null && r.ParentName.ToLower().Contains(searchText)) ||
                            (r.ParentPatronymic != null && r.ParentPatronymic.ToLower().Contains(searchText)))
                        .GroupBy(r => r.Id)
                        .Select(g => new StudentGroup // Используем тот же класс, что и в основном отображении
                        {
                            Id = g.Key,
                            StudentSurName = g.First().StudentSurName,
                            StudentName = g.First().StudentName,
                            StudentPatronymic = g.First().StudentPatronymic,
                            BirthDate = g.First().BirthDate ?? DateTime.MinValue,
                            ParentFullName = g.First().ParentSurName != null
                                ? $"{g.First().ParentSurName} {g.First().ParentName} {g.First().ParentPatronymic}"
                                : "Нет данных",
                            Sports = g.Where(x => x.SportName != null)
                                     .Select(x => x.SportName)
                                     .Distinct()
                                     .ToList()
                        }).ToList();

                    dateStydent.ItemsSource = filteredResult;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        // Метод для обновления DataGrid всеми данными
        private void RefreshDataGrid()
        {
            var query = from s in App.DataBase.Student
                        join ss in App.DataBase.SportsStudents on s.Id equals ss.IdStudent into ssGroup
                        from ss in ssGroup.DefaultIfEmpty()
                        join sp in App.DataBase.Sports on ss.IdSport equals sp.Id into spGroup
                        from sp in spGroup.DefaultIfEmpty()
                        join p in App.DataBase.Parents on s.IdParents equals p.Id into pGroup
                        from p in pGroup.DefaultIfEmpty()
                        select new
                        {
                            Id = s.Id,
                            StudentSurName = s.SurName,
                            StudentName = s.Name,
                            StudentPatronymic = s.Patronymic,
                            BirthDate = s.BirthDate, // Оставляем как есть (DateTime?)
                            SportName = sp != null ? sp.Name : null,
                            ParentSurName = p != null ? p.SurName : null,
                            ParentName = p != null ? p.Name : null,
                            ParentPatronymic = p != null ? p.Patronymic : null
                        };

            // Группируем по студентам
            var groupedStudents = query.ToList()
                .GroupBy(s => s.Id)
                .Select(g => new StudentGroup
                {
                    Id = g.Key,
                    StudentSurName = g.First().StudentSurName,
                    StudentName = g.First().StudentName,
                    StudentPatronymic = g.First().StudentPatronymic,
                    BirthDate = g.First().BirthDate ?? DateTime.MinValue, // Обработка nullable
                    ParentFullName = g.First().ParentSurName != null
                        ? $"{g.First().ParentSurName} {g.First().ParentName} {g.First().ParentPatronymic}"
                        : "Нет данных",
                    Sports = g.Where(x => x.SportName != null)
                             .Select(x => x.SportName)
                             .Distinct()
                             .ToList()
                }).ToList();

            dateStydent.ItemsSource = groupedStudents;
        }

        private void btnAdStydent_Click(object sender, RoutedEventArgs e)
        {
            GlobalVarbinary.StatysPage = 0;
            NavigeteManager.StartFrame.Navigate(new EditStydentPage(0));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(dateStydent.SelectedItem is StudentGroup selectedStudent)) return;

            if (MessageBox.Show("Вы точно хотите удалить ученика?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var studentToDelete = App.DataBase.Student.Find(selectedStudent.Id);
                if (studentToDelete == null) return;

                App.DataBase.Student.Remove(studentToDelete);
                App.DataBase.SaveChanges();

                MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateStydent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dateStydent.SelectedItem != null)
                {
                    var selectedItem = (StudentGroup) dateStydent.SelectedItem;

                    // Получаем оригинальную запись студента из базы данных
                    var idStud = App.DataBase.Student.FirstOrDefault(c =>
                        c.SurName == selectedItem.StudentSurName &&
                        c.Name == selectedItem.StudentName &&
                        c.Patronymic == selectedItem.StudentPatronymic &&
                        c.BirthDate == selectedItem.BirthDate);

                    if (idStud != null)
                    {
                        GlobalVarbinary.StatysPage = 1;

                        NavigeteManager.StartFrame.Navigate(new EditStydentPage(idStud.Id));
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

                RefreshDataGrid();
            }

            if (GlobalVarbinary.StatysPage == 0)
            {
                RefreshDataGrid();
            }
        }
    }
}
