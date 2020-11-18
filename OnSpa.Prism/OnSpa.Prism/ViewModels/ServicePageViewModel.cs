using Prism.Navigation;

namespace OnSpa.Prism.ViewModels
{
    public class ServicePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ServicePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Service";
        }
    }
}
