namespace Users.Common.IoC
{
    public interface IServiceView
    {
        void RegisterTypeView<ViewModel, View>();

        IServiceView Window<ViewModel>(string[] identifier = null, params object[] args) where ViewModel : class;

        void NonModal();
        void Close<ViewModel>();
    }
}