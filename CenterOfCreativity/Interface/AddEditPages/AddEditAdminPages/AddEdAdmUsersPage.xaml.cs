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
using CenterOfCreativity.BaseModel;
using CenterOfCreativity.Classes;

namespace CenterOfCreativity.Interface.AddEditPages.AddEditAdminPages
{
    /// <summary>
    /// Логика взаимодействия для AddEdAdmUsersPage.xaml
    /// </summary>
    public partial class AddEdAdmUsersPage : Page
    {
        private User _currentUser = new User();

        public AddEdAdmUsersPage(User selectedUsers)
        {
            InitializeComponent();

            if (selectedUsers != null)
            {
                _currentUser = selectedUsers;
            }

            DataContext = _currentUser;

            comBoxRoles.ItemsSource = App.DataContext.Role.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.FirstName))
            {
                errors.AppendLine("Укажите имя");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.LastName))
            {
                errors.AppendLine("Укажите фамилию");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.Login))
            {
                errors.AppendLine("Укажите логин");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
            {
                errors.AppendLine("Укажите пароль");
            }
            if (_currentUser.Role == null)
            {
                errors.AppendLine("Укажите роль");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentUser.Id == 0)
            {
                string user = null;/*App.DataContext.User.Find(_currentUser.Login);*/
                if (user == null)
                {
                    App.DataContext.User.Add(_currentUser);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            try
            {
                App.DataContext.SaveChanges();
                MessageBox.Show("Информация сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Util.BaseFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
