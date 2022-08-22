using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class MonthDay
    {
        public DateTime Date { get; set; }

        private ObservableCollection<Event> events;

        public MonthDay()
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
