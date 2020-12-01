
using OnSpa.Common.Helpers;
using OnSpa.Common.Models;
using OnSpa.Prism.Helpers;
using OnSpa.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace OnSpa.Prism.ItemViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (PageName == "LoginPage" && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.Token = null;
            }

            if (IsLoginRequired && !Settings.IsLogin)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginFirstMessage, Languages.Accept);
                NavigationParameters parameters = new NavigationParameters
        {
            { "pageReturn", PageName }
        };

                await _navigationService.NavigateAsync($"/{nameof(OnSpaMasterDetailPage)}/NavigationPage/{nameof(LoginPage)}", parameters);
            }
            else
            {
                await _navigationService.NavigateAsync($"/{nameof(OnSpaMasterDetailPage)}/NavigationPage/{PageName}");
            }
        }

    }

}
