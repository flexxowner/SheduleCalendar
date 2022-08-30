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
        public string Month { get; set; }
        public ObservableCollection<string> DaysOfWeek { get; set; }
        public ObservableCollection<MonthDay> CalendarDays { get; set; }
    }
}
