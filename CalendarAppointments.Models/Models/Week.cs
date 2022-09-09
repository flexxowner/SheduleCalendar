using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class Week
    {
        public ObservableCollection<Event> Events { get; set; }

        public IEnumerable<string> Hours { get; set; }

        public DateTime Today { get; set; }

        public int WeekNumber { get; set; }

        public System.DayOfWeek DayOfWeek { get; set; }

    }
}
