using OnSpa.Common.Models;
using OnSpa.Common.Responses;
using OnSpa.Common.Services;
using OnSpa.Prism.Helpers;
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
        private bool _isRunning;
        private string _search;
        private List<ServiceType> _myServiceType;
        private DelegateCommand _searchCommand;


        public ServiceTypePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.ServiceType;

            LoadProductsAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowServicesType));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowServicesType();
            }
        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
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
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<ServiceType>(
                 url,
                "/api",
                "/ServiceTypes/GetServiceTypes");
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            _myServiceType = (List<ServiceType>)response.Result;
            ShowServicesType();

        }

        private void ShowServicesType()
        {
            if (string.IsNullOrEmpty(Search))
            {
                ServiceTypes = new ObservableCollection<ServiceType>(_myServiceType);
            }
            else
            {
                ServiceTypes = new ObservableCollection<ServiceType>(_myServiceType
                .Where(p => p.Name.ToLower().Contains(Search.ToLower())));
            }
        }

    }
}
