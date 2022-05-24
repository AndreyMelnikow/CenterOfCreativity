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

namespace CenterOfCreativity.Interface.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminHistoryPage.xaml
    /// </summary>
    public partial class AdminHistoryPage : Page
    {
        public AdminHistoryPage()
        {
            InitializeComponent();

            Update();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.DataContext.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridLoginHistory.ItemsSource = App.DataContext.LoginHistory.ToList();

                Update();

                dGridLoginHistory.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
            }
        }

        private void tBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            List<LoginHistory> currentHistory = App.DataContext.LoginHistory.ToList();
            dGridLoginHistory.ItemsSource = currentHistory.Where(p =>
            p.User.FirstName.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.User.LastName.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.User.Login.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.User.Role.Name.ToLower().Contains(tBSearch.Text.ToLower())).ToList();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show
                    ($"Вы хотите удалить {App.DataContext.LoginHistory.ToList().Count} записей?", "Удаление", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    App.DataContext.LoginHistory.RemoveRange(App.DataContext.LoginHistory.ToList());
                    App.DataContext.SaveChanges();
                    dGridLoginHistory.ItemsSource = App.DataContext.LoginHistory.ToList();
                    MessageBox.Show("Данные удалены", "Удаление");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
