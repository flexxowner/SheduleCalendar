using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ExternalConnectors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Calendar = System.Globalization.Calendar;

namespace CalendarAppointments.ViewModel.Services
{
    public static class DataChanger
    {
        public static void ChangeMonthForward(ref int CurrentMonth, int Max, int Min, ref int CurrentYear)
        {
            CurrentMonth++;

            if (CurrentMonth > Max)
            {
                CurrentYear++;
                CurrentMonth = Min;
            }
            else if (CurrentMonth < Min)
            {
                CurrentYear--;
                CurrentMonth = Max;
            }
        }

        public static string DateToStringLower(DateTime today, string Format, CultureInfo myCulture)
        {
           var month = today.ToString(Format, myCulture.DateTimeFormat).ToLower();
            return month;
        }

        public static int GetWeek(Calendar cal, DateTime today)
        {
            var week = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            return week;
        }

        public static string DateToStringUpper(DateTimeOffset today, string Format, string Culture)
        {
            var month = today.ToString(Format, CultureInfo.CreateSpecificCulture(Culture)).ToUpper();
            return month;
        }

        public static List<DateTime> GetHours()
        {
           var hours = (List<DateTime>)Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i))).ToList();
           return hours;
        }

        public static void ChangeMonthBack(ref int CurrentMonth, int Max, int Min, ref int CurrentYear)
        {
            CurrentMonth--;

            if (CurrentMonth > Max)
            {
                CurrentYear++;
                CurrentMonth = Min;
            }
            else if (CurrentMonth < Min)
            {
                CurrentYear--;
                CurrentMonth = Max;
            }
        }

        public static DateTime ChangeTodayBack(DateTime today)
        {
            today = today.AddDays(-1);
            return today;
        }

        public static DateTime ChangeTomorrowBack(DateTime tomorrow)
        {
            tomorrow = tomorrow.AddDays(-1);
            return tomorrow;
        }

        public static DateTime ChangeTodayForward(DateTime today)
        {
            today = today.AddDays(1);
            return today;
        }

        public static DateTime ChangeTomorrowForward(DateTime tomorrow)
        {
            tomorrow = tomorrow.AddDays(1);
            return tomorrow;
        }

        public static DateTime ChangeDateBack(DateTime currentDate, int min)
        {
            currentDate = new DateTime(currentDate.Year - min, min, min);
            return currentDate;
        }

        public static DateTime ChangeDateForward(DateTime currentDate, int min)
        {
            currentDate = new DateTime(currentDate.Year + min, min, min);
            return currentDate;
        }
    }
}
