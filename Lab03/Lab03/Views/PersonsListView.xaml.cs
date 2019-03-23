using System.Windows;
using System.Windows.Controls;
using Lab03.Models;
using Lab03.Tools.Managers;
using Lab03.Tools.Navigation;
using Lab03.VIewModels;

namespace Lab03.Views
{
    /// <summary>
    /// Interaction logic for PersonsListView.xaml
    /// </summary>
    public partial class PersonsListView : UserControl, INavigatable
    {
        public PersonsListView()
        {
            InitializeComponent();
            DataContext = new PersonsListViewModel();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            Person person = (Person)_dataGrid.SelectedItem;
            if (person != null)
            {
                StationManager.CurrentPerson = person;
                StationManager.DataStorage.RemovePerson(person);
                StationManager._editing = true;
                NavigationManager.Instance.Navigate(ViewType.PersonEdit);
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var p in _dataGrid.SelectedItems)
            {
                if(p != null)
                StationManager.DataStorage.RemovePerson((Person)p);
            }
        }
    }
}
