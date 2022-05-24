using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterOfCreativity.BaseModel
{
    partial class Group
    {
        public int CountOfMembers
        {
            get
            {
                return App.DataContext.Member.Where(p => p.IdGroup == Id).ToList().Count;
            }
        }
    }
}
