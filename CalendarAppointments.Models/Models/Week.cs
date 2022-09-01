using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class Week
    {
        public DateTime Today { get; set; }
        public int WeekNumber { get; set; }
        public System.DayOfWeek DayOfWeek { get; set; }
        public IEnumerable<string> Hours { get; set; }
        public ObservableCollection<Event> Events { get; set; }
    }
}
