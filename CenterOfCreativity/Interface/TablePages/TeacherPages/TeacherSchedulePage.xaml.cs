using CenterOfCreativity.BaseModel;
using CenterOfCreativity.Classes;
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

namespace CenterOfCreativity.Interface.TablePages.TeacherPages
{
    /// <summary>
    /// Логика взаимодействия для TeacherSchedulePage.xaml
    /// </summary>
    public partial class TeacherSchedulePage : Page
    {
        public TeacherSchedulePage()
        {
            InitializeComponent();

            Update();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Update();

                App.DataContext.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridSchedule.ItemsSource = App.DataContext.Schedule
                    .Where(p => p.IdUser == Util.UserId && p.Date >= DateTime.Now).ToList();

                dGridSchedule.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
            }
        }

        private void tBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void cBWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBWeek.SelectedIndex == 0)
            {
                dGridSchedule.ItemsSource = App.DataContext.Schedule.ToList();
            }
            else if (cBWeek.SelectedIndex == 1)
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = DateTime.Now.Date;
                int startDays = (int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Monday;
                int endDays = (int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Friday;

                if (startDays > 0)
                {
                    startDate = DateTime.Now.AddDays(-startDays).Date;
                }
                if (endDays < 0)
                {
                    endDate = DateTime.Now.AddDays(-endDays).Date;
                }

                dGridSchedule.ItemsSource = App.DataContext.Schedule.Where
                    (p => p.Date >= startDate && p.Date <= endDate &&
                    p.User.IdRole == 3 && p.User.IsActive).ToList();
            }
            else if (cBWeek.SelectedIndex == 2)
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = DateTime.Now.Date;
                int startDays = (int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Monday;
                int endDays = (int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Friday;

                if (startDays > 0)
                {
                    startDate = DateTime.Now.AddDays(-startDays + 7).Date;
                }
                else
                {
                    startDate = startDate.AddDays(7);
                }
                if (endDays < 0)
                {
                    endDate = DateTime.Now.AddDays(-endDays + 7).Date;
                }
                else
                {
                    endDate = endDate.AddDays(7);
                }

                dGridSchedule.ItemsSource = App.DataContext.Schedule.Where
                    (p => p.Date >= startDate && p.Date <= endDate &&
                    p.User.IdRole == 3 && p.User.IsActive).ToList();
            }
        }

        private void Update()
        {
            List<Schedule> currentSchedule = App.DataContext.Schedule.ToList();
            dGridSchedule.ItemsSource = currentSchedule.Where(p =>
            p.Group.Name.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Event.Name.ToLower().Contains(tBSearch.Text.ToLower())).ToList();
        }

        private void btnDate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dPickerEndDate.Text) && !string.IsNullOrWhiteSpace(dPickerStartDate.Text))
            {
                dGridSchedule.ItemsSource = App.DataContext.Schedule
                    .Where(p => p.Date >= dPickerStartDate.SelectedDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(dPickerEndDate.Text) && string.IsNullOrWhiteSpace(dPickerStartDate.Text))
            {
                dGridSchedule.ItemsSource = App.DataContext.Schedule
                    .Where(p => p.Date <= dPickerEndDate.SelectedDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(dPickerEndDate.Text) && !string.IsNullOrWhiteSpace(dPickerStartDate.Text))
            {
                dGridSchedule.ItemsSource = App.DataContext.Schedule
                    .Where(p => p.Date >= dPickerStartDate.SelectedDate
                    && p.Date <= dPickerEndDate.SelectedDate).ToList();
            }
            else
            {
                MessageBox.Show("Введите даты!", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
