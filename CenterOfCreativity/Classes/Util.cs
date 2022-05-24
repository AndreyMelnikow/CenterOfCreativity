using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CenterOfCreativity.Classes
{
    public static class Util
    {
        public static string UserName { get; set; }
        public static string UserLogin { get; set; }
        public static int UserRole { get; set; }
        public static int UserId { get; set; }

        public static string CurrentPage { get; set; } = "Авторизация";

        public static bool IsDelLogEntries { get; set; }

        public static Frame BaseFrame { get; set; }
    }
}
