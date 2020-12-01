using OnSpa.Prism.Helpers;
using Prism.Navigation;

namespace OnSpa.Prism.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        // private readonly INavigationService _navigationService;
        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Customer;
        }
    }
}
