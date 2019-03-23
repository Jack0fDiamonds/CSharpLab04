using System.Windows.Controls;
using Lab03.Tools.Navigation;
using Lab03.VIewModels;

namespace Lab03.Views
{
    /// <summary>
    /// Interaction logic for PersonCreationView.xaml
    /// </summary>
    public partial class PersonCreationView : UserControl, INavigatable
    {
        public PersonCreationView()
        {
            InitializeComponent();
            DataContext = new PersonCreationViewModel();
        }
    }
}
