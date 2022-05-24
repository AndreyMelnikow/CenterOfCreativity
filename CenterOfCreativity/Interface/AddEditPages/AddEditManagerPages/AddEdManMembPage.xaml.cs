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
    /// Логика взаимодействия для AddEdManVisPage.xaml
    /// </summary>
    public partial class AddEdManMembPage : Page
    {
        private Member _currentMember = new Member();

        public AddEdManMembPage(Member selectedMember)
        {
            InitializeComponent();

            if (selectedMember != null)
            {
                _currentMember = selectedMember;
            }

            DataContext = _currentMember;

            cBGroups.ItemsSource = App.DataContext.Group.ToList();

            dPickerDateOfBirth.Text = DateTime.Now.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentMember.FirstName))
            {
                errors.AppendLine("Укажите имя");
            }
            if (string.IsNullOrWhiteSpace(_currentMember.LastName))
            {
                errors.AppendLine("Укажите фамилию");
            }
            if (!string.IsNullOrWhiteSpace(dPickerDateOfBirth.Text))
            {
                DateTime dateOfBirth = DateTime.Parse(dPickerDateOfBirth.Text);
                _currentMember.DateOfBirth = DateTime.Parse(dateOfBirth.ToString("yyyy/MM/dd"));
            }
            else
            {
                errors.AppendLine("Укажите дату рождения");
            }
            if (_currentMember.Group == null)
            {
                errors.AppendLine("Укажите Группу");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentMember.Id == 0)
            {
                App.DataContext.Member.Add(_currentMember);
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
