using OnSpa.Common.Models;
using OnSpa.Common.Responses;
using OnSpa.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace OnSpa.Prism.ViewModels
{
    public class ServiceTypePageViewModel : ViewModelBase

    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<ServiceType> _ServiceType;

        public ServiceTypePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "ServiceTypes";
            LoadProductsAsync();
        }
        public ObservableCollection<ServiceType> ServiceTypes
        {
            get => _ServiceType;
            set => SetProperty(ref _ServiceType, value);
        }

        private async void LoadProductsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<ServiceType>(
                 url,
                "/api",
                "/ServiceTypes/GetServiceTypes/ "+ serviceId);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            List<ServiceType> myServiceType = (List<ServiceType>)response.Result;
            ServiceTypes = new ObservableCollection<ServiceType>(myServiceType);
        }

    }
}
