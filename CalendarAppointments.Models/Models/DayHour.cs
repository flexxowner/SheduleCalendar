using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class DayHour
    {
        public DateTime Hour { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<Event> Events { get; set; }

        public DayHour()
        {
            Events = new ObservableCollection<Event>();
        }

        public string HourString
        {
            get { return this.Hour.ToString("HH:mm"); }
        }
    }
}
