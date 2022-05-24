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

namespace CenterOfCreativity.Interface.AddEditPages.AddEditManagerPages
{
    /// <summary>
    /// Логика взаимодействия для AddEdManSchedPage.xaml
    /// </summary>
    public partial class AddEdManSchedPage : Page
    {
        private Schedule _currentSchedule = new Schedule();

        public AddEdManSchedPage(Schedule selectedSchedule)
        {
            InitializeComponent();

            cBTime.ItemsSource = GetTimeList();

            dPickerDate.Text = DateTime.Today.ToString();

            if (selectedSchedule != null)
            {
                _currentSchedule = selectedSchedule;

                dPickerDate.Text = selectedSchedule.Date.ToString();

                cBTime.SelectedItem = GetTimeList().Find(p => p == selectedSchedule.StartEndTime);
            }

            DataContext = _currentSchedule;

            cBEvents.ItemsSource = App.DataContext.Event.ToList();
            cBGroups.ItemsSource = App.DataContext.Group.ToList();
            cBAuditory.ItemsSource = App.DataContext.Auditory.ToList();
            cBTeachers.ItemsSource = App.DataContext.User.Where(p => p.IdRole == 3).ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _currentSchedule.StartEndTime = cBTime.SelectedItem as string;

            StringBuilder errors = new StringBuilder();

            if (_currentSchedule.Group == null)
            {
                errors.AppendLine("Укажите Группу");
            }

            else
            {
                _currentSchedule.IdGroup = (cBGroups.SelectedItem as Group).Id;
            }

            if (_currentSchedule.Event == null)
            {
                errors.AppendLine("Укажите мероприятие");
            }
            else
            {
                _currentSchedule.IdEvent = (cBEvents.SelectedItem as Event).Id;
            }

            if (!string.IsNullOrWhiteSpace(dPickerDate.Text))
            {
                DateTime dateOfBirth = DateTime.Parse(dPickerDate.Text);
                _currentSchedule.Date = DateTime.Parse(dateOfBirth.ToString("yyyy/MM/dd"));
            }
            else
            {
                errors.AppendLine("Укажите дату");
            }

            if (_currentSchedule.StartEndTime == null)
            {
                errors.AppendLine("Укажите время");
            }

            if (_currentSchedule.Auditory == null)
            {
                errors.AppendLine("Укажите аудиторию");
            }
            else
            {
                _currentSchedule.IdAuditory = (cBAuditory.SelectedItem as Auditory).Id;
            }

            if (_currentSchedule.User == null)
            {
                errors.AppendLine("Укажите преподавателя");
            }
            else
            {
                _currentSchedule.IdUser = (cBTeachers.SelectedItem as User).Id;
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var temp = _currentSchedule;
            if (App.DataContext.Schedule.Any(p => p.Date == temp.Date
            && p.StartEndTime == temp.StartEndTime && p.IdUser == temp.IdUser && temp.Id == 0))
            {
                errors.AppendLine("Преподаватель на данное время занят!");
            }
            else if (App.DataContext.Schedule.Where(p => p.Date == temp.Date
             && p.StartEndTime == temp.StartEndTime && p.IdUser == temp.IdUser).ToList().Count > 1)
            {
                errors.AppendLine("Преподаватель на данное время занят!");
            }

            if (App.DataContext.Schedule.Any(p => p.Date == temp.Date
            && p.StartEndTime == temp.StartEndTime && p.IdAuditory == temp.IdAuditory && temp.Id == 0))
            {
                errors.AppendLine("Аудитория на данное время занята!");
            }
            else if (App.DataContext.Schedule.Where(p => p.Date == temp.Date
            && p.StartEndTime == temp.StartEndTime && p.IdAuditory == temp.IdAuditory).ToList().Count > 1)
            {
                errors.AppendLine("Аудитория на данное время занята!");
            }

            if (App.DataContext.Schedule.Any(p => p.Date == temp.Date
            && p.StartEndTime == temp.StartEndTime && p.IdGroup == temp.IdGroup && temp.Id == 0))
            {
                errors.AppendLine("Группа на данное время занята!");
            }
            else if (App.DataContext.Schedule.Where(p => p.Date == temp.Date
            && p.StartEndTime == temp.StartEndTime && p.IdGroup == temp.IdGroup).ToList().Count > 1)
            {
                errors.AppendLine("Группа на данное время занята!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentSchedule.Id == 0)
            {
                App.DataContext.Schedule.Add(_currentSchedule);
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

        private List<string> GetTimeList()
        {
            List<string> timeList = new List<string>
            {
                "08:00 - 09:00",
                "09:00 - 10:00",
                "10:00 - 11:00",
                "11:00 - 12:00",
                "12:00 - 13:00",
                "13:00 - 14:00",
                "14:00 - 15:00",
                "15:00 - 16:00",
                "16:00 - 17:00",
                "17:00 - 18:00"
            };

            return timeList;
        }
    }
}
