using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab03.Models;
using Lab03.Tools;
using Lab03.Tools.Managers;
using Lab03.Tools.Navigation;

namespace Lab03.VIewModels
{
    class PersonsListViewModel:BaseViewModel
    {
        private ObservableCollection<Person> _persons;

        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private Task _update;

        public ObservableCollection<Person> Persons
        {
            get
            {
                return _persons;
            }
            private set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addPersonCommand;
        private ICommand _editPersonCommand;
        private ICommand _deletePersonCommand;

        public PersonsListViewModel()
        {
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartUpdateTask();
            StationManager.StopThreads += StopUpdateTask;
        }

        private void StartUpdateTask()
        {
            _update = Task.Factory.StartNew(UpdateTask, TaskCreationOptions.LongRunning);
        }

        private void UpdateTask()
        {
            while (!_token.IsCancellationRequested)
            {
                Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
                for (int i = 0; i < 4; i++)
                {
                    Thread.Sleep(250);
                    if (_token.IsCancellationRequested)
                        break;
                }
                if (_token.IsCancellationRequested)
                    break;
            }
        }

        internal void StopUpdateTask()
        {
            _tokenSource.Cancel();
            _update.Wait(2000);
            _update.Dispose();
            _update = null;
        }

        public ICommand AddPersonCommand
        {
            get { return _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(AddPersonImplementation)); }
        }
        public ICommand EditPersonCommand
        {
            get { return _editPersonCommand ?? (_editPersonCommand = new RelayCommand<object>(EditPersonImplementation)); }
        }
        public ICommand DeletePersonCommand
        {
            get { return _deletePersonCommand ?? (_deletePersonCommand = new RelayCommand<object>(DeletePersonImplementation)); }
        }

        private void AddPersonImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.PersonCreation);
        }
        private void EditPersonImplementation(object obj)
        {
        }
        private void DeletePersonImplementation(object obj)
        {
        }
    }
}
