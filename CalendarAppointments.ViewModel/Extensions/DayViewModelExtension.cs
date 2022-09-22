using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class DayViewModelExtension
    {
        public static void AddHours(this ObservableCollection<DayHour> dayHours, List<DateTime> hours, DateTime Today)
        {
            DateFiller.AddHours(dayHours, hours, Today);
        }

        public static void ReadEventsFromFile(this ObservableCollection<DayHour> hours, string path)
        {
            EventService.ReadEventsFromFile(hours, path);
        }
    }
}
