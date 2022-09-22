using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class Week
    {
        private ObservableCollection<DayHour> hours;

        public Week()
        {
            hours = new ObservableCollection<DayHour>();
        }

        public ObservableCollection<DayHour> Hours
        { 
            get => hours;
            set => hours = value;
        }

        public DateTime Date { get; set; }

        public int WeekNumber { get; set; }

    }
}
