using System;
using System.Collections.Generic;
using System.IO;
using Lab03.Models;
using Lab03.Tools.Managers;

namespace Lab03.Tools.DataStorae
{
    class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _persons;
        private Random _random = new Random();

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
                for (int i = 0; i < 50; i++)
                {
                    _persons.Add(new Person("FirstName" + (i < 10 ? "0" : "") + i, "LastName" + (i < 10 ? "0" : "") + i, "email" + (i < 10 ? "0" : "") + i + "@email.com", RandomDate()));
                }
            }
        }

        private DateTime RandomDate()
        {
            DateTime start = new DateTime(1940, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
            Save();
        }

        public void RemovePerson(Person person)
        {
            _persons.Remove(person);
        }

        public List<Person> PersonsList
        {
            get { return _persons; }
        }

        public void Save()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }
    }
}
