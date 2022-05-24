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
using CenterOfCreativity.Interface.AddEditPages;

namespace CenterOfCreativity.Interface.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для ManagerGroupsPage.xaml
    /// </summary>
    public partial class ManagerGroupsPage : Page
    {
        public ManagerGroupsPage()
        {
            InitializeComponent();
            Update();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                dGridGroups.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                App.DataContext.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridGroups.ItemsSource = App.DataContext.Group.ToList();
                Update();
            }
        }

        private void tBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            List<Group> currentGroups = App.DataContext.Group.ToList();
            dGridGroups.ItemsSource = currentGroups.Where(p =>
            p.Name.ToLower().Contains(textBoxSearch.Text.ToLower())).ToList();
        }
    }
}
