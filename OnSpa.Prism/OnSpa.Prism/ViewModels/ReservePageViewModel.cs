using Newtonsoft.Json;
using OnSpa.Common.Helpers;
using OnSpa.Common.Responses;
using OnSpa.Common.Services;
using OnSpa.Prism.Helpers;
using OnSpa.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnSpa.Prism.ViewModels
{
    public class ReservePageViewModel : ViewModelBase
    {

        private INavigationService _navigationService;
        private IApiService _apiService;
        private ObservableCollection<AppointmentItemViewModel> _appointment;
        private bool _isRefreshing;
        private DelegateCommand _refreshAppointmentsCommand;
        public ReservePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            LoadAppointment();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Refresh"))
            {
                LoadAppointment();
            }
        }


        public DelegateCommand RefreshAppointmentCommand => _refreshAppointmentsCommand ?? (_refreshAppointmentsCommand = new DelegateCommand(LoadAppointment));

        public ObservableCollection<AppointmentItemViewModel> Appointment
        {
            get => _appointment;
            set => SetProperty(ref _appointment, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private async void LoadAppointment()
        {
            IsRefreshing = true;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var user = token.User;

            var response = await _apiService.GetAgendaForCustomer(url, "/api", "/Appointments/GetAgendaForCustomer", user.Email, "bearer", token.Token);
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var myAppointment = (List<AppointmentResponse>)response.Result;
            Appointment = new ObservableCollection<AppointmentItemViewModel>(myAppointment.Select(a => new AppointmentItemViewModel(_navigationService)
            {
                Date = a.Date,
                Id = a.Id,
                IsActive = a.IsActive,
                User = a.User,
                Service = a.Service
            }).ToList());

            IsRefreshing = false;
        }
    }
}