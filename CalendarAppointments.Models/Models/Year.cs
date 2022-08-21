using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class Year
    {
        public int Month { get; set; }
        public int CurrentYear { get; set; }
        public ObservableCollection<string> DaysOfWeek { get; set; }
        public int Day { get; set; }
        public ObservableCollection<Week> Weeks { get; set; }
    }
}
