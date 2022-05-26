using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using CenterOfCreativity.Classes;
using CenterOfCreativity.BaseModel;
using CenterOfCreativity.Interface.ManagerPages;
using CenterOfCreativity.Interface.AdminPages;
using CenterOfCreativity.Interface.TablePages.TeacherPages;

namespace CenterOfCreativity.Interface
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private int _countOfFail = 0;

        public AuthPage()
        {
            InitializeComponent();

            CheckConnectionAsync();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tBLogin.Text != "") && (pBPassword.Password != ""))
                {
                    var list = App.DataContext.User.Where(p => p.Login == tBLogin.Text).ToList();
                    if (list.Count != 0 && (pBPassword.Password == list[0].Password || tBShowPass.Text == list[0].Password))
                    {
                        Util.CurrentUser = list[0];

                        if (list[0].IdRole == 1)
                        {
                            RecordLogIn(list[0].Id, true);
                            _countOfFail = 0;

                            MessageBox.Show("Авторизация прошла успешно!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);

                            Util.BaseFrame.Navigate(new AdminHistoryPage());
                            Util.IsDelLogEntries = true;
                            Util.CurrentPage = "История входа";

                            return;
                        }
                        else if (list[0].IdRole == 2)
                        {
                            RecordLogIn(list[0].Id, true);
                            _countOfFail = 0;

                            MessageBox.Show("Авторизация прошла успешно!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);

                            Util.BaseFrame.Navigate(new ManagerSchedulePage());
                            Util.IsDelLogEntries = true;
                            Util.CurrentPage = "Расписание мероприятий";

                            return;
                        }
                        else if (list[0].IdRole == 3)
                        {
                            RecordLogIn(list[0].Id, true);
                            _countOfFail = 0;

                            MessageBox.Show("Авторизация прошла успешно!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);

                            Util.BaseFrame.Navigate(new TeacherSchedulePage());
                            Util.IsDelLogEntries = true;
                            Util.CurrentPage = "Расписание мероприятий";

                            return;
                        }
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            RecordLogIn(list[0].Id, false);
                        }
                        MessageBox.Show("Неверный логин или пароль!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                        _countOfFail++;
                    }

                    if (_countOfFail == 3)
                    {
                        MessageBox.Show("Превышено допустимое число попыток входа, подождите 10 секунд и попробуйте снова.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                        BlockAuthAsync();
                    }
                }
                else if ((tBLogin.Text == "") && (pBPassword.Password != ""))
                {
                    MessageBox.Show("Не заполнено поле логина!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if ((tBLogin.Text != "") && (pBPassword.Password == ""))
                {
                    MessageBox.Show("Не заполнено поле пароля!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if ((tBLogin.Text == "") && (pBPassword.Password == ""))
                {
                    MessageBox.Show("Не заполнены поля логина и пароля!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Отсутсвует связь с базой");
            }
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            btnPass.Click += PassHidden_Click;
            btnPass.Click -= btnPass_Click;
            tBShowPass.Text = pBPassword.Password;

            pBPassword.Visibility = Visibility.Collapsed;
            tBShowPass.Visibility = Visibility.Visible;
        }

        private void PassHidden_Click(object sender, RoutedEventArgs e)
        {
            btnPass.Click += btnPass_Click;
            btnPass.Click -= PassHidden_Click;
            pBPassword.Password = tBShowPass.Text;

            pBPassword.Visibility = Visibility.Visible;
            tBShowPass.Visibility = Visibility.Collapsed;
        }

        private void btnRetryCon_Click(object sender, RoutedEventArgs e)
        {
            btnRetryCon.Visibility = Visibility.Collapsed;
            CheckConnectionAsync();
        }

        private async void BlockAuthAsync()
        {
            btnAuth.IsEnabled = false;
            await Task.Run(() => Thread.Sleep(10000));
            await Dispatcher.BeginInvoke(new ThreadStart(delegate { btnAuth.IsEnabled = true; }));
        }

        private void RecordLogIn(int idUser, bool isSuccessful)
        {
            try
            {
                App.DataContext.LoginHistory.Add(new LoginHistory
                {
                    IdUser = idUser,
                    Date = DateTime.Now,
                    IsSuccessful = isSuccessful
                });
                App.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private async void CheckConnectionAsync()
        {
            tBConnection.Visibility = Visibility.Visible;
            btnAuth.IsEnabled = false;
            btnPass.IsEnabled = false;
            tBLogin.IsEnabled = false;
            tBShowPass.IsEnabled = false;
            pBPassword.IsEnabled = false;

            try
            {
                App.DataContext.Database.Connection.Close();
                await App.DataContext.Database.Connection.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                tBConnection.Visibility = Visibility.Collapsed;
                btnRetryCon.Visibility = Visibility.Visible;
                return;
            }

            tBConnection.Visibility = Visibility.Collapsed;
            await Dispatcher.BeginInvoke(new ThreadStart(delegate { 
                btnAuth.IsEnabled = true;
                btnPass.IsEnabled = true;
                tBLogin.IsEnabled = true; 
                tBShowPass.IsEnabled = true; 
                pBPassword.IsEnabled = true; }));
        }
    }
}