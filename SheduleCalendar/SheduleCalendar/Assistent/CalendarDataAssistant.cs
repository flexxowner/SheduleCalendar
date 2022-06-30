using SheduleCalendar.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Assistent
{
    public class CalendarDataAssistant
    {
        public static IEnumerable<MonthModel> GetMonthsDays(int year, int month, DataTemplateSelector itemTemplateSelector = null)
        {
            var firstDayOfWeekDate = GetFirstDayOfWeekDate(new DateTime(year, month, 1));
            return GetMonthsDaysFromDate(firstDayOfWeekDate, itemTemplateSelector);
        }

        static DateTime GetFirstDayOfWeekDate(DateTime monthStartDate)
        {
            return monthStartDate.AddDays(0 - GetDaysFromWeekStart(monthStartDate));
        }

        static int GetDaysFromWeekStart(DateTime monthStartDate)
        {
            var mondayIsFirstWeekDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday;
            if (CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday && monthStartDate.DayOfWeek == DayOfWeek.Sunday) return 6;
            return (int)monthStartDate.DayOfWeek + (mondayIsFirstWeekDay ? -1 : 0);
        }

        public static IEnumerable<MonthModel> GetMonthsDaysFromDate(DateTime startDate, DataTemplateSelector itemTemplateSelector = null)
        {
            var days = new List<MonthModel>();
            var date = startDate;
            var columnIndex = 0;
            var rowIndex = 0;
            var currentMonth = date.Day == 1 ? date.Month : (date.Month < 12 ? date.Month + 1 : 1);
            for (var i = 0; i < 42; i++)
            {
                if (columnIndex == 7)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
                var dayItem = new MonthModel
                {
                    Date = date,
                    ColumnIndex = columnIndex,
                    RowIndex = rowIndex,
                    IsInOtherView = date.Month != currentMonth
                };
                days.Add(dayItem);
                date = date.AddDays(1);
                columnIndex++;
            }
            return days;
        }
    }
}
