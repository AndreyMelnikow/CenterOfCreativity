using CenterOfCreativity.BaseModel;
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
using CenterOfCreativity.Classes;
using CenterOfCreativity.Interface.AddEditPages.AddEditManagerPages;

namespace CenterOfCreativity.Interface.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для ManagerVisitorsPage.xaml
    /// </summary>
    public partial class ManagerMembersPage : Page
    {
        public ManagerMembersPage()
        {
            InitializeComponent();
            Update();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.DataContext.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridVisitors.ItemsSource = App.DataContext.Member.ToList();
                Update();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new AddEdManMembPage((sender as Button).DataContext as Member));
            Util.CurrentPage = "Редактирование данных о посетителе";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new AddEdManMembPage(null));
            Util.CurrentPage = "Добавление данных о посетителе";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var membersForRemoving = dGridVisitors.SelectedItems.Cast<Member>().ToList();
            MessageBoxResult mesBoxRes = MessageBox.Show($"Вы точно хотите удалить следующие {membersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (mesBoxRes == MessageBoxResult.Yes)
            {
                try
                {
                    App.DataContext.Member.RemoveRange(membersForRemoving);
                    App.DataContext.SaveChanges();
                    MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    dGridVisitors.ItemsSource = App.DataContext.Member.ToList();
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
            List<Member> currentVisitors = App.DataContext.Member.ToList();

            foreach (var row in currentVisitors)
            {
                if (row.Patronymic == null)
                {
                    row.Patronymic = "";
                }
            }

            dGridVisitors.ItemsSource = currentVisitors.Where(p =>
            p.FirstName.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.LastName.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Patronymic.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Group.Name.ToLower().Contains(tBSearch.Text.ToLower())).ToList();
        }
    }
}
