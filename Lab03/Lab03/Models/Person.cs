using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Lab03.Exceptions;
using Lab03.Tools.Managers;

namespace Lab03.Models
{
    [Serializable]
    internal class Person
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;
        private string _birthString;
        private string _sunSign;
        private string _chineseSign;
        private bool _isAdult;
        private bool _isBirthday;
        private int _age;

        private readonly string[] chineseZodiacSigns = new string[] { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
        }

        public string BirthString
        {
            get { return _birthString; }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
        }

        public bool IsBirthday
        {
            get { return _isBirthday; }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public int Age
        {
            get { return _age; }
        }

        private string DetermineWesternZodiac()
        {
            int day = _birthDate.DayOfYear;
            if (day > 355 || day < 20)
                return "Capricorn";
            else if (day < 50)
                return "Aquarius";
            else if (day < 80)
                return "Pisces";
            else if (day < 110)
                return "Aries";
            else if (day < 141)
                return "Taurus";
            else if (day < 172)
                return "Gemini";
            else if (day < 204)
                return "Cancer";
            else if (day < 235)
                return "Leo";
            else if (day < 266)
                return "Virgo";
            else if (day < 296)
                return "Libra";
            else if (day < 326)
                return "Scorpio";
            return "Sagittarius";
        }

        private async void InitProperties()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                _birthString = _birthDate.Year + "." + (_birthDate.Month < 10 ? "0" : "") + _birthDate.Month + "." + (_birthDate.Day < 10 ? "0" : "") + _birthDate.Day;
                _age = DateTime.Today.Year - _birthDate.Year - (_birthDate.DayOfYear > DateTime.Today.DayOfYear ? 1 : 0);
                _sunSign = DetermineWesternZodiac();
                _chineseSign = chineseZodiacSigns[_birthDate.Year % 12];
                _isAdult = _age >= 18;
                _isBirthday = DateTime.Today.DayOfYear == _birthDate.DayOfYear;
            });
            LoaderManager.Instance.HideLoader();
        }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = birthDate;
            InitProperties();
            ValidatePersonData(this);
        }

        public Person(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = DateTime.Now;
            InitProperties();
            ValidatePersonData(this);
        }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
            _email = "";
            InitProperties();
            ValidatePersonData(this);
        }

        private void ValidatePersonData(Person person)
        {
            Thread.Sleep(1);
            if (!IsEmailValid(_email)) throw new InvalidEmailException();
            if (Age < 0) throw new UnbornPersonException();
            if (Age > 135) throw new DeadPersonException();
        }

        private bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
