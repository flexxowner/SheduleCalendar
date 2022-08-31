using System;
using System.Collections.ObjectModel;

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
