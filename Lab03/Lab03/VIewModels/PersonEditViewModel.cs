using Lab03.Exceptions;
using Lab03.Models;
using Lab03.Tools;
using Lab03.Tools.Managers;
using Lab03.Tools.Navigation;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab03.VIewModels
{
    class PersonEditViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private bool _canProceedAfterEditing;

        private ICommand _proceedCommand;

        public string FirstName
        {
            get
            {
                if (StationManager._editing)
                {
                    _firstName = StationManager.CurrentPerson.FirstName;
                }
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get
            {
                if (StationManager._editing)
                {
                    _lastName = StationManager.CurrentPerson.LastName;
                }
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get
            {
                if (StationManager._editing)
                {
                    StationManager._editing = false;
                    _canProceedAfterEditing = true;
                    _email = StationManager.CurrentPerson.Email;
                }
                return _email;
            }
            set
            {
                _email = value;
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
            if (_canProceedAfterEditing)
                return true;
            return !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email);
        }

        private async void ProceedImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(250);
                try
                {
                    var person = new Person(_firstName, _lastName, _email, StationManager.CurrentPerson.BirthDate);
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
                _canProceedAfterEditing = false;
                NavigationManager.Instance.Navigate(ViewType.PersonsList);
            }
        }
    }
}
