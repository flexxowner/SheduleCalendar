using CalendarAppointments.Models.Models;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace CalendarAppointments.ViewModel.Service
{
    public static class DateFiller
    {
        private const int Max = 13;

        public static void AddDates(
            this ObservableCollection<ListModel> dates, int Year, string firstPath, string secondPath)
        {
            for (var i = 1; i < Max; i++)
            {
                var month = new DateTime(Year, i, 1);
                var days = new ObservableCollection<MonthDay>();
                for (var j = 1; j < DateTime.DaysInMonth(Year, month.Month) + 1; j++)
                {
                    days.Add(new MonthDay() { Date = new DateTime(Year, i, j) });
                }
                dates.Add(new ListModel() { Date = month, Dates = days });
                EventService.ReadEventsFromFile(firstPath, days);
                EventService.ReadEventsFromFile(secondPath, days);
            }
        }

        public static void AddDates(
            this ObservableCollection<Year> years, int Year, string path, string secondPath, string format, string culture)
        {
            for (int i = 1; i < Max; i++)
            {
                var month = new DateTime(Year, i, 1);
                var days = new ObservableCollection<YearDay>();
                for (int j = 1; j < DateTime.DaysInMonth(Year, month.Month) + 1; j++)
                {
                    days.Add(new YearDay() { Date = new DateTime(Year, i, j) });
                    ColorService.AddColorForToday(days);
                }
                DayOfWeekService.GetListOfAbbreviatedWeekDays(Year, i);
                years.Add(new Year() { Month = month.ToString(format, CultureInfo.CreateSpecificCulture(culture)), DaysOfWeek = DayOfWeekService.DaysOfWeek, CalendarDays = days });
                EventService.ReadEventsFromFile(path, years);
                EventService.ReadEventsFromFile(secondPath, years);
            }
        }

        public static void AddMonths(
            ObservableCollection<CalendarMonth> months, DateTimeOffset today, string format, string culture)
        {
            for (int i = 0; i < months.Count; i++)
            {
                months[i] = new CalendarMonth() { Month = today.ToString(format, CultureInfo.CreateSpecificCulture(culture)).ToUpper(), Year = today.Year };
            }
        }

        public static void AddDaysOfMonth(ObservableCollection<MonthDay> calendarDays, int CurrentYear, int CurrentMonth)
        {
            calendarDays.Clear();

            for (int i = 1; i < DateTime.DaysInMonth(CurrentYear, CurrentMonth) + 1; i++)
            {
                calendarDays.Add(new MonthDay { Date = new DateTime(CurrentYear, CurrentMonth, i) });
            }
        }

        public static ObservableCollection<DayHour> AddHours(ObservableCollection<DayHour> dayHours, List<DateTime> hours, DateTime Today)
        {
            if (dayHours != null)
            {
                dayHours.Clear();
                foreach (var hour in hours)
                {
                    dayHours.Add(new DayHour() { Hour = hour, Date = Today.Date.Date });
                }
            }
            else if (dayHours == null)
            {
                foreach (var hour in hours)
                {
                    dayHours.Add(new DayHour() { Hour = hour, Date = Today.Date.Date });
                }
            }
            return dayHours;
        }

        public static ObservableCollection<DateTime> AddDaysOfWeek(ObservableCollection<DateTime> listOfDays, DateTime startDate, DateTime endDate)
        {
            do
            {
                startDate = startDate.AddDays(1);
                listOfDays.Add(startDate);
            } while (startDate.DayOfWeek != endDate.DayOfWeek);

            return listOfDays;
        }

        public static void AddDateToWeekList(ObservableCollection<Week> weeks, ObservableCollection<DateTime> listOfDays)
        {
            foreach (var item in listOfDays)
            {
                weeks.Add(new Week() { Date = item.Date});
            }

        }

        public static void AddHoursToWeekDays(ObservableCollection<Week> weeks, ObservableCollection<DayHour> dayHours, List<DateTime> hours)
        {
            for (var i = 0; i < weeks.Count; i++)
            {
                var dailyHours = AddHours(dayHours, hours, weeks[i].Date);
                for (var j = 0; j < dailyHours.Count; j++)
                {
                    weeks[i].Hours.Add(dailyHours[j]);
                }
            }
        }
    }
}
