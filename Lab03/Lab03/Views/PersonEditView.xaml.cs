using System.Windows.Controls;
using Lab03.Tools.Navigation;
using Lab03.VIewModels;

namespace Lab03.Views
{
    /// <summary>
    /// Interaction logic for PersonEditView.xaml
    /// </summary>
    public partial class PersonEditView : UserControl, INavigatable
    {
        public PersonEditView()
        {
            InitializeComponent();
            DataContext = new PersonEditViewModel();
        }
    }
}
