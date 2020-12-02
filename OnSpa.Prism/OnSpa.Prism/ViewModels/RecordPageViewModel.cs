using Newtonsoft.Json;
using OnSpa.Common.Helpers;
using OnSpa.Common.Models;
using OnSpa.Common.Request;
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
    public class RecordPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiService _apiService;
        private ObservableCollection<Appointment> _appointments;
        private bool _isRunning;
        private bool _isEnabled;
        public RecordPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Record;
            LoadRecords();
        }

        public ObservableCollection<Appointment> Appointments {
            get => _appointments;
            set => SetProperty(ref _appointments, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void LoadRecords()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var request = new HistoryRequest { CustomerId = token.User.Id };
            var url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<Appointment>(url, "/api", "/Appointments/HistoryByCustomer/"+ token.User.Email);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error, Languages.Accept);
                return;
            }

            List<Appointment> appointments = (List<Appointment>)response.Result;
            Appointments = new ObservableCollection<Appointment>(appointments);
        }
    }
}
