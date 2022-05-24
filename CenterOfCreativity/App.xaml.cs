using CenterOfCreativity.BaseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CenterOfCreativity
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CenterOfCreativityEntities DataContext { get; } = new CenterOfCreativityEntities();
    }
}
