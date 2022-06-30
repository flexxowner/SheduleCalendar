using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SheduleCalendar.Assistent;
using SheduleCalendar.Model;
using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.ViewModels
{
    public class MonthViewModel : MonthDataDesign
    {
        public MonthViewModel() : base(6, 2022)
        {
        }
    }
}
