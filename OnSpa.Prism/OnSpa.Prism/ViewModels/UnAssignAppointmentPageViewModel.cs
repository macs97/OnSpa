using Newtonsoft.Json;
using OnSpa.Common.Helpers;
using OnSpa.Common.Request;
using OnSpa.Common.Responses;
using OnSpa.Common.Services;
using OnSpa.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Prism.ViewModels
{
    public class UnAssignAppointmentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _cancelCommand;
        private AppointmentResponse _appointment;
        private bool _isRunning;
        private bool _isEnabled;

        public UnAssignAppointmentPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Title = Languages.CancelAppointmentMessage;
        }

        public AppointmentResponse Appointment
        {
            get => _appointment;
            set => SetProperty(ref _appointment, value);
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

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Appointment"))
            {
                Appointment = parameters.GetValue<AppointmentResponse>("Appointment");
            }
        }

        private async void Cancel()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.CancelAppointmentMessage,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var request = new UnAssignRequest { AppointmentId = Appointment.Id };
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await _apiService.PostAsync(url, "/api", "/Appointments/UnAssignAppointment", request, "bearer", token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var parameters = new NavigationParameters
            {
                { "Refresh", true }
            };

            await _navigationService.GoBackAsync(parameters);
        }
    }
}
