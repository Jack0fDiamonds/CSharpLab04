using Lab03.Tools.DataStorae;
using System;
using Lab03.Models;
using System.Windows;
using System.Collections.Generic;

namespace Lab03.Tools.Managers
{
    static class StationManager
    {
        public static event Action StopThreads;

        private static IDataStorage _dataStorage;

        internal static Person CurrentPerson { get; set; }

        public static bool _editing = false;

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static List<Person> Test
        {
            get { return DataStorage.PersonsList; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            DataStorage.Save();
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
