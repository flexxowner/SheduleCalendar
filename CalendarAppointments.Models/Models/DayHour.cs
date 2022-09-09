using System;
using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class DayHour
    {
        private readonly ObservableCollection<Event> events;

        public DayHour()
        {
            events = new ObservableCollection<Event>();
        }
        public DateTime Hour { get; set; }

        public DateTime Date { get; set; }

        public string HourString
        {
            get => this.Hour.ToString("HH:mm");
        }

        public ObservableCollection<Event> Events
        {
            get => events;
        }
    }
}
