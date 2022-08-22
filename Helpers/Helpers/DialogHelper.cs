using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Helpers.Helpers
{
    public static class DialogHelper
    {
        public static async void WarningDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Warning!",
                Content = "The start time of the meeting must be earlier than the end time.",
                CloseButtonText = "Ok"
            };
            await dialog.ShowAsync();
        }
        public static async void SubjectEmptyWarning()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Warning!",
                Content = "Please enter the subject.",
                CloseButtonText = "Ok"
            };
            await dialog.ShowAsync();
        }
        public static async void ErrorDialog(Exception exception)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Error",
                Content = $"Something happened: {exception.Message}",
                CloseButtonText = "Ok"
            };
            await dialog.ShowAsync();
        }
    }
}
