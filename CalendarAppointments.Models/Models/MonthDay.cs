using System;
using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class MonthDay
    {
        private readonly ObservableCollection<Event> events;

        public MonthDay()
        {
            events = new ObservableCollection<Event>();
        }

        public DateTime Date { get; set; }

        public ObservableCollection<Event> Events
        {
            get => events;
        }
    }
}
