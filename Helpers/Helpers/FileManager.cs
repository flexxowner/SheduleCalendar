using CalendarAppointments.Models.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml.Shapes;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Helpers.Helpers
{
    public class FileManager
    {
        public static async void WriteToFile(ObservableCollection<Event> events, string path)
        {
            string rootFrameDataString = ObjectSerializer<ObservableCollection<Event>>.ToXml(events);
            if (!string.IsNullOrEmpty(rootFrameDataString))
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(path, CreationCollisionOption.OpenIfExists);
                await FileIO.WriteTextAsync(localFile, rootFrameDataString);
            }
        }
        public static async void Delete(string path)
        {
            StorageFolder currentFolder = ApplicationData.Current.LocalFolder;
            string name = path;
            StorageFile manifestFile = await currentFolder.GetFileAsync(name);
            await manifestFile.DeleteAsync();
        }
    }
}
