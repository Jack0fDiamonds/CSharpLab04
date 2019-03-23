using Lab03.Models;
using System.Collections.Generic;

namespace Lab03.Tools.DataStorae
{
    interface IDataStorage
    {
        void AddPerson(Person person);
        void RemovePerson(Person person);
        void Save();
        List<Person> PersonsList { get; }
    }
}
