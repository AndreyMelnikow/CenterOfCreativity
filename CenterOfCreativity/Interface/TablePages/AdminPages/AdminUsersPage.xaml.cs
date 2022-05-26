using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CenterOfCreativity.BaseModel;
using CenterOfCreativity.Classes;
using CenterOfCreativity.Interface.AddEditPages.AddEditAdminPages;

namespace CenterOfCreativity.Interface.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminUsersPage.xaml
    /// </summary>
    public partial class AdminUsersPage : Page
    {
        public AdminUsersPage()
        {
            InitializeComponent();
            Update();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.DataContext.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridUsers.ItemsSource = App.DataContext.User.ToList();
                Update();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new AddEdAdmUsersPage(null));
            Util.CurrentPage = "Добавление пользователя";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new AddEdAdmUsersPage((sender as Button).DataContext as User));
            Util.CurrentPage = "Редактирование пользователя";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = dGridUsers.SelectedItems.Cast<User>().ToList();
            MessageBoxResult mesBoxRes = MessageBox.Show($"Вы точно хотите удалить следующие {usersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (mesBoxRes == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var user in usersForRemoving)
                    {
                        if (user.Login == Util.CurrentUser.Login)
                        {
                            MessageBox.Show("Нельзя удалить пользователя под которым вы находитесь!", "Удаление", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    App.DataContext.User.RemoveRange(usersForRemoving);
                    App.DataContext.SaveChanges();
                    MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    dGridUsers.ItemsSource = App.DataContext.User.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void tBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            List<User> currentUsers = App.DataContext.User.ToList();

            foreach (var row in currentUsers)
            {
                if (row.Patronymic == null)
                {
                    row.Patronymic = "";
                }
            }

            dGridUsers.ItemsSource = currentUsers.Where(p =>
            p.FirstName.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.LastName.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Login.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Role.Name.ToLower().Contains(tBSearch.Text.ToLower())).ToList();
        }
    }
}
