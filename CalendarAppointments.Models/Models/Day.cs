using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class Day
    {
        public DateTime Today { get; set; }
        public DateTime Tomorrow { get; set; }
        public DateTime Hour { get; set; }
        public ObservableCollection<Event> Events { get; set; }

        public Day()
        {
            Events = new ObservableCollection<Event>();
        }

        public string HourString
        {
            get { return this.Hour.ToString("HH:mm"); }
        }
    }
}
