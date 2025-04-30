using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using SportsSchool.Logic;
using SportsSchool.Entities;

namespace SportsSchool.Pages.AddEdditPage
{
    /// <summary>
    /// Логика взаимодействия для AdUesrPage.xaml
    /// </summary>
    public partial class AdUesrPage : Page
    {
        public static int RandomNumber { get; set; } = 0;

        private Users _curentUsers;
        public AdUesrPage(Users selectUsers)
        {
            InitializeComponent();

            if (selectUsers != null)
            {
                _curentUsers = selectUsers;
            }
            DataContext = _curentUsers;


            if (GlobalVarbinary.CheckRoot == 1)
            {
                stakRoot.Visibility = Visibility.Visible;
            }

            if (GlobalVarbinary.CheckRoot == 0)
            {
               stakRoot.Visibility = Visibility.Hidden;
            }
            cbRole.ItemsSource = App.DataBase.Role.ToList();
        }

        private  void SendEmailAsync()
        {
            if (tbMail.Text.Length == 0)
            {
                MessageBox.Show("Введите почту!", "Информация",
                           MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (RandomNumber != 0)
            {
                MessageBox.Show("Код уже отправлен на почту!", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Random random = new Random();
            RandomNumber = random.Next(100000, 999999);

            // чтение конфига
            var senderData = SenderData.FromJson("uniq_key.json");

            string toAddress = tbMail.Text;

            string subject = "Регистрация в программе спорт школа";

            
            string text = $"Вы пытаетесь зарегистрироваться в программе спорт школа." +
                $" Введите этот код {RandomNumber}";

            // MESSAGE SETTINGS

            var fromMail = new MailAddress(senderData.SenderMail, senderData.SenderName);

            var toMail = new MailAddress(toAddress);

            MailMessage message = new MailMessage(fromMail, toMail)
            {
                Subject = subject,
                Body = text,
                IsBodyHtml = false
            };

            // SMTP CLIENT SETTINGS

            SmtpClient smtpClient = new SmtpClient (SmtpSettings.Host, SmtpSettings.Port)
            {
                Credentials = new NetworkCredential(senderData.SenderMail, senderData.Key),
                EnableSsl = true,
                Timeout = 10000
            };

            // MESSAGE SENDING

            try
            {
                smtpClient.Send(message);
                MessageBox.Show("Введите код отправленный вам на почту", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправлении письма", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private  void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isEmpty = FormValidator.AreFieldsEmpty(tbSurName, tbName, tbOthestvo, tbLogin, tbPassword, tbMail);
            Users users = new Users();
            users.Name = tbName.Text;
            users.SurName = tbSurName.Text;
            users.MiddleName = tbOthestvo.Text;
            users.IdRole = cbRole.SelectedIndex;
            users.Login = tbLogin.Text;
            users.Password = tbPassword.Text;
            users.Mail = tbMail.Text;

            if(GlobalVarbinary.StatysPage == 0)
            {
                if (GlobalVarbinary.CheckRoot == 1)
                {               
                    if (isEmpty || cbRole.SelectedIndex == -1)
                    {
                        MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                   
                    try
                    {
                        App.DataBase.Users.Add(users);
                        App.DataBase.SaveChanges();

                        MessageBox.Show("Пользователь добавлен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigeteManager.StartFrame.GoBack();
                  
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.ToString()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                if (GlobalVarbinary.CheckRoot == 0)
                {
                
                    if (isEmpty)
                    {
                        MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if(tbKey.Text != RandomNumber.ToString())
                    {
                        MessageBox.Show("Код не совпадает, проверьте свою почту !", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    try
                    {
                        users.IdRole = 2;
                        App.DataBase.Users.Add(users);
                        App.DataBase.SaveChanges();

                        MessageBox.Show("Вы успешно зарегистрировались", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigeteManager.StartFrame.GoBack();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.ToString()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }

            if (GlobalVarbinary.StatysPage == 1)
            {
                if (_curentUsers.Login != "")
                {
                    try
                    {
                        App.DataBase.SaveChanges();

                        MessageBox.Show("Данные отредактированы");
                        NavigeteManager.StartFrame.GoBack();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void btnSMS_Click(object sender, RoutedEventArgs e)
        {
            SendEmailAsync();
        }
    }
}
