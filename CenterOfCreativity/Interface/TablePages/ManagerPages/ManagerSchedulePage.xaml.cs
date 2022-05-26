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
    /// Логика взаимодействия для ManagerSchedulePage.xaml
    /// </summary>
    public partial class ManagerSchedulePage : Page
    {
        public ManagerSchedulePage()
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
                dGridSchedule.ItemsSource = App.DataContext.Schedule.ToList();

                dGridSchedule.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var scheduleForRemoving = dGridSchedule.SelectedItems.Cast<Schedule>().ToList();
            MessageBoxResult mesBoxRes = MessageBox.Show($"Вы точно хотите удалить следующие {scheduleForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (mesBoxRes == MessageBoxResult.Yes)
            {
                try
                {
                    App.DataContext.Schedule.RemoveRange(scheduleForRemoving);
                    App.DataContext.SaveChanges();
                    MessageBox.Show("Данные удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    dGridSchedule.ItemsSource = App.DataContext.Schedule.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new AddEdManSchedPage(null));
            Util.CurrentPage = "Добавление расписания";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new AddEdManSchedPage((sender as Button).DataContext as Schedule));
            Util.CurrentPage = "Редактирование расписания";
        }
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Util.BaseFrame.Navigate(new ManagerPrintPage());
            Util.CurrentPage = "Вывод на печать";
        }

        private void tBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        
        private void btnCreateScheduleList_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Составить расписание на следующую неделю?", "Расписание",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var existSchedule = App.DataContext.Schedule.ToList();
                for (int i = 0; i < existSchedule.Count; i++)
                {
                    bool isExist = existSchedule.Any(p => p.Date >= DateTime.Now 
                    && p.Date == existSchedule[i].Date.AddDays(7));
                    if (isExist)
                    {
                        MessageBox.Show("Расписание на следующую неделю уже составлено!", "Расписание",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                }

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

                var schedule = App.DataContext.Schedule.Where
                    (p => p.Date >= startDate && p.Date <= endDate && 
                    p.User.IdRole == 3 && p.User.IsActive).ToList();

                if (!schedule.Any())
                {
                    MessageBox.Show("Расписание на следующую неделю уже составлено!", "Расписание",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    return;
                }

                schedule.ForEach(p => p.Date = p.Date.AddDays(7));

                try
                {
                    App.DataContext.Schedule.AddRange(schedule);
                    App.DataContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                dGridSchedule.ItemsSource = App.DataContext.Schedule.ToList();
            }
        }
        private void cBWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBWeek.SelectedIndex == 0)
            {
                dGridSchedule.ItemsSource = App.DataContext.Schedule.ToList();

                dGridSchedule.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
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

                dGridSchedule.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
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

                dGridSchedule.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
            }
        }

        private void Update()
        {
            List<Schedule> currentSchedule = App.DataContext.Schedule.ToList();
            dGridSchedule.ItemsSource = currentSchedule.Where(p =>
            p.Group.Name.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Event.Name.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.Auditory.Name.ToLower().Contains(tBSearch.Text.ToLower()) ||
            p.User.FullName.ToLower().Contains(tBSearch.Text.ToLower())).ToList();
        }

        private void btnDate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dPickerEndDate.Text) && !string.IsNullOrWhiteSpace(dPickerStartDate.Text))
            {
                dGridSchedule.ItemsSource = App.DataContext.Schedule
                    .Where(p => p.Date >=  dPickerStartDate.SelectedDate).ToList();
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
