using System;

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
