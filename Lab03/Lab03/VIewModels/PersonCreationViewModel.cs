using Lab03.Tools.Managers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab03.Models;
using Lab03.Tools;
using Lab03.Tools.Navigation;
using Lab03.Exceptions;

namespace Lab03.VIewModels
{
    class PersonCreationViewModel:BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate = DateTime.Today;

        private ICommand _proceedCommand;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand ProceedCommand
        {
            get
            {
                return _proceedCommand ??
                       (_proceedCommand = new RelayCommand<object>(ProceedImplementation, CanProceed));
            }
        }

        private bool CanProceed(object obj)
        {
            return !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email) &&
                   _birthDate != null;
        }

        private async void ProceedImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(250);
                try
                {
                    var person = new Person(_firstName, _lastName, _email, _birthDate);
                    StationManager.DataStorage.AddPerson(person);
                    StationManager.CurrentPerson = person;

                    if (person.IsBirthday)
                    {
                        MessageBox.Show("Happy Birthday!!!");
                    }

                    return true;
                }
                catch (InvalidEmailException)
                {
                    MessageBox.Show($"Error\nEmail {Email} is invalid");
                }
                catch (UnbornPersonException)
                {
                    MessageBox.Show("Error\nThis person has not been born yet");
                }
                catch (DeadPersonException)
                {
                    MessageBox.Show("Error\nThis person is already dead");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error\nCould not create this person");
                }

                return false;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                NavigationManager.Instance.Navigate(ViewType.PersonsList);
            }
        }
    }
}