using SheduleCalendar.Assistent;
using SheduleCalendar.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.ViewModels
{
    public class MonthDataDesign 
    {
        public MonthDataDesign(int month, int year, DataTemplateSelector itemTemplateSelector = null)
        {
            Month = month;
            Year = year;
            LoadDays(itemTemplateSelector);
        }

        void LoadDays(DataTemplateSelector itemTemplateSelector = null)
        {
            Days = CalendarDataAssistant.GetMonthsDays(Year, Month, itemTemplateSelector);
        }

        public IEnumerable<MonthModel> Days { get; private set; }

        public int Month { get; private set; }

        public int Year { get; private set; }

        public override string ToString()
        {
            return new DateTime(Year, Month, 1).ToString("MMMM yyyy", CultureInfo.CurrentCulture);
        }

    }
}

