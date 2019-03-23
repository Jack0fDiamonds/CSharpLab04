namespace Lab03.Tools.Navigation
{
    internal enum ViewType
    {
        PersonCreation,
        PersonEdit,
        PersonsList
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
