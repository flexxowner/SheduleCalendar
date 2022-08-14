using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class CalendarDay
    {
        public int Number { get; set; }

        private ObservableCollection<Event> events;

        public CalendarDay()
        {
            events = new ObservableCollection<Event>();
        }

        public ObservableCollection<Event> Events
        {
            get { return events; }
            set { events = value; }
        }
    }
}
