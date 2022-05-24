using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CenterOfCreativity.Interface
{
    /// <summary>
    /// Логика взаимодействия для ChangePassPage.xaml
    /// </summary>
    public partial class ChangePassPage : Page
    {
        public ChangePassPage()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (pBOldPass.Password != "" && pBNewPass.Password != "" && pBRePass.Password != "")
            {
                var user = App.DataContext.User.Find(Util.UserId);
                string pass = user.Password;
                if (pBOldPass.Password == pass && pBNewPass.Password == pBRePass.Password)
                {
                    user.Password = pBNewPass.Password;
                    App.DataContext.Entry(user).State = EntityState.Modified;
                    App.DataContext.SaveChanges();
                    MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
