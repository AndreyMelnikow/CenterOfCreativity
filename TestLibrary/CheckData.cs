using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public static class CheckData
    {
        public static bool ValidateData(int idUser, int idGroup, int idEvent, 
            int idAuditory, string date, string startEndTime)
        {
            if (idUser > 0 && idGroup > 0 && idEvent > 0 && idAuditory > 0 
                && date != null && startEndTime != null)
            {
                try
                {
                    DateTime.Parse(date);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
