using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class DayHours
    {
        public DateTime Hour { get; set; }
        public DateTime CurrentDate { get; set; }
        public ObservableCollection<Event> Events { get; set; }

        public DayHours()
        {
            Events = new ObservableCollection<Event>();
        }

        public string HourString
        {
            get { return this.Hour.ToString("HH:mm"); }
        }
    }
}
