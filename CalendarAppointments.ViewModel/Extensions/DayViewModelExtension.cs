using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class DayViewModelExtension
    {
        public static void AddHours(this ObservableCollection<DayHour> dayHours, List<DateTime> hours, DateTime Today, DateTime Tomorrow)
        {
            DateFiller.AddHours(dayHours, hours, Today, Tomorrow);
        }

        public static void ReadEventsFromFile(this ObservableCollection<Event> events, ObservableCollection<DayHour> hours, string path, DateTime Today, DateTime Tomorrow)
        {
            EventService.ReadEventsFromFile(events, hours, path, Today, Tomorrow);
        }
    }
}
