using CalendarAppointments.Models.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Helpers.Helpers
{
    public class FileManager
    {
        public static async void CreateFile(string path)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(path, CreationCollisionOption.OpenIfExists);
        }
        
        public static async void SaveToExistingFile(ObservableCollection<Event> events, string path)
        {
            string rootFrameDataString = ObjectSerializer<ObservableCollection<Event>>.ToXml(events);
            if (!string.IsNullOrEmpty(rootFrameDataString))
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync(path);
                await FileIO.WriteTextAsync(storageFile, rootFrameDataString);
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
