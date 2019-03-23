using System;
using Lab03.Views;

namespace Lab03.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.PersonCreation:
                    ViewsDictionary.Add(viewType, new PersonCreationView());
                    break;
                    break;
                case ViewType.PersonsList:
                    ViewsDictionary.Add(viewType, new PersonsListView());
                    break;
                case ViewType.PersonEdit:
                    ViewsDictionary.Add(viewType, new PersonEditView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
