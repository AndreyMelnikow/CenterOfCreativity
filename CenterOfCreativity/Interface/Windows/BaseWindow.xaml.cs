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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CenterOfCreativity.Classes;
using CenterOfCreativity.BaseModel;
using CenterOfCreativity.Interface;
using CenterOfCreativity.Interface.ManagerPages;
using CenterOfCreativity.Interface.AdminPages;

namespace CenterOfCreativity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {

        private string _role = "";
        private int _index = 0;

        public BaseWindow()
        {
            InitializeComponent();
            baseFrame.Navigate(new AuthPage());
            Util.BaseFrame = baseFrame;
        }

        private void baseFrame_ContentRendered(object sender, EventArgs e)
        {
            textBlockPage.Text = Util.CurrentPage;
            if (Util.UserRole == 1)
            {
                _role = "Администратор";
            }
            else if (Util.UserRole == 2)
            {
                _role = "Менеджер";
            }
            if (_role == "Менеджер")
            {
                mItemVisitors.Visibility = Visibility.Visible;
                mItemSchedule.Visibility = Visibility.Visible;
                mItemGroupsManager.Visibility = Visibility.Visible;
                mItemEventManager.Visibility = Visibility.Visible;
            }
            else if (_role == "Администратор")
            {
                mItemHistory.Visibility = Visibility.Visible;
                mItemEventAdmin.Visibility = Visibility.Visible;
                mItemGroupsAdmin.Visibility = Visibility.Visible;
                mItemUsers.Visibility = Visibility.Visible;
            }
            if (baseFrame.Content.GetType().Name == "AuthPage" )
            {
                while (baseFrame.CanGoBack)
                {
                    baseFrame.NavigationService.RemoveBackEntry();
                }

                tBlockUser.Text = "";
                tBlockUser.Visibility = Visibility.Collapsed;

                btnMenu.Visibility = Visibility.Collapsed;
                menuMain.Visibility = Visibility.Collapsed;
                gridMenu.Visibility = Visibility.Collapsed;

                mItemVisitors.Visibility = Visibility.Collapsed;
                mItemSchedule.Visibility = Visibility.Collapsed;
                mItemGroupsManager.Visibility = Visibility.Collapsed;
                mItemEventManager.Visibility = Visibility.Collapsed;

                mItemHistory.Visibility = Visibility.Collapsed;
                mItemEventAdmin.Visibility = Visibility.Collapsed;
                mItemGroupsAdmin.Visibility = Visibility.Collapsed;
                mItemUsers.Visibility = Visibility.Collapsed;

                btnMenu.Click -= HiddenMenu_Click;
                btnMenu.Click -= btnMenu_Click;
                btnMenu.Click += btnMenu_Click;

                ResizeMode = ResizeMode.NoResize;
                MinWidth = 800;
                MinHeight = 520;
                Width = MinWidth;
                Height = MinHeight;
                WindowState = WindowState.Normal;
            }
            else
            {
                tBlockUser.Text = _role + " " + Util.UserName;

                tBlockUser.Visibility = Visibility.Visible;
                btnMenu.Visibility = Visibility.Visible;

                ResizeMode = ResizeMode.CanResize;

                if (Util.IsDelLogEntries == true)
                {
                    MinWidth = 1000;
                    MinHeight = 700;
                    Width = MinWidth;
                    Height = MinHeight;
                }
            }
            if (Util.IsDelLogEntries == true)
            {
                baseFrame.NavigationService.RemoveBackEntry();
                Util.IsDelLogEntries = false;
            }
            if (baseFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Collapsed;
            }

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            menuMain.Visibility = Visibility.Visible;
            DoubleAnimation showMenu = new DoubleAnimation 
            {
                From = 1,
                To = 200,
                Duration = TimeSpan.FromSeconds(0.15)
            };
            menuMain.BeginAnimation(WidthProperty, showMenu);
            gridMenu.Visibility = Visibility.Visible;
            btnMenu.Click += HiddenMenu_Click;
            btnMenu.Click -= btnMenu_Click;
        }

        private void HiddenMenu_Click(object sender, EventArgs e)
        {
            HiddenMenuAnim();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _index++;
            baseFrame.GoBack();
        }

        private void menuItemClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dlgRes = MessageBox.Show("Завершить работу в приложении?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dlgRes == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dlgRes = MessageBox.Show("Выйти из учётной записи?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dlgRes == MessageBoxResult.Yes)
            {
                baseFrame.Navigate(new AuthPage());
                while (baseFrame.CanGoBack)
                {
                    baseFrame.NavigationService.RemoveBackEntry();
                }
            }
        }

        private void menuItemUsers_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new AdminUsersPage());
            Util.CurrentPage = "Список пользователей";
            HiddenMenuAnim();
        }

        private void menuItemHistory_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new AdminHistoryPage());
            Util.CurrentPage = "История входа";
            HiddenMenuAnim();
        }

        private void menuItemGroupsAdmin_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new AdminGroupsPage());
            Util.CurrentPage = "Список групп";
            HiddenMenuAnim();
        }

        private void menuItemEventAdmin_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new AdminEventPage());
            Util.CurrentPage = "Список мероприятий";
            HiddenMenuAnim();
        }

        private void menuItemVisitors_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new ManagerMembersPage());
            Util.CurrentPage = "Список посетителей";
            HiddenMenuAnim();
        }

        private void menuItemSchedule_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new ManagerSchedulePage());
            Util.CurrentPage = "Расписание мероприятий";
            HiddenMenuAnim();
        }

        private void menuItemGroupsManager_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new ManagerGroupsPage());
            Util.CurrentPage = "Список групп";
            HiddenMenuAnim();
        }

        private void menuItemEventManager_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new ManagerEventPage());
            Util.CurrentPage = "Список мероприятий";
            HiddenMenuAnim();
        }

        private void menuItemChangePass_Click(object sender, RoutedEventArgs e)
        {
            baseFrame.Navigate(new ChangePassPage());
            Util.CurrentPage = "Смена пароля";
            HiddenMenuAnim();
        }

        private async void HiddenMenuAnimAsync()
        {
            await Task.Run(() => Thread.Sleep(160));
            await Dispatcher.BeginInvoke(new ThreadStart(delegate { menuMain.Visibility = Visibility.Collapsed; }));
        }

        private void HiddenMenuAnim()
        {
            DoubleAnimation hiddenMenu = new DoubleAnimation 
            {
                From = 200,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.15)
            };
            menuMain.BeginAnimation(WidthProperty, hiddenMenu);
            gridMenu.Visibility = Visibility.Collapsed;
            btnMenu.Click += btnMenu_Click;
            btnMenu.Click -= HiddenMenu_Click;
            HiddenMenuAnimAsync();
        }

        private void gridMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HiddenMenuAnim();
        }
    }
}