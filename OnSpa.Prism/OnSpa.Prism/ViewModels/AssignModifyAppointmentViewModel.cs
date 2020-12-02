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
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace OnSpa.Prism.ViewModels
{
    public class AssignModifyAppointmentViewModel : ViewModelBase
    {

        private AppointmentResponse _appointment;
        private ServiceResponse _service;
        private ObservableCollection<ServiceResponse> _services;
        private bool _isRunning;
        private bool _isEnabled;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _assignCommand;
        private DelegateCommand _cancelCommand;


        public AssignModifyAppointmentViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = Languages.AssignModifyAppointment;
            IsEnabled = true;
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public DelegateCommand AssignCommand => _assignCommand ?? (_assignCommand = new DelegateCommand(Assign));

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));


        public AppointmentResponse Appointment
        {
            get => _appointment;
            set => SetProperty(ref _appointment, value);
        }

        public ServiceResponse Service
        {
            get => _service;
            set => SetProperty(ref _service, value);
        }

        public ObservableCollection<ServiceResponse> Services
        {
            get => _services;
            set => SetProperty(ref _services, value);
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Appointment"))
            {
                Appointment = parameters.GetValue<AppointmentResponse>("Appointment");
                LoadServices();
            }
        }

        private void LoadServices()
        {
            var user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            //Services = new ObservableCollection<ServiceResponse>(user.Ser);
            //Pet = Pets.FirstOrDefault(p => p.Id == _agenda.Pet.Id);
        }

        private async void Assign()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            var request = new AssignRequest
            {
                AppointmentId = Appointment.Id,
                UserId = user.Id,
                ServiceId = Service.Id
            };

            var response = await _apiService.PostAsync(url, "/api", "/Appointment/AssignAppointment", request, "bearer", token.Token);

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

        private async Task<bool> ValidateData()
        {
            if (Service == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ServiceError, Languages.Accept);
                return false;
            }

            return true;
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
            var response = await _apiService.PostAsync(url, "/api", "/Appointment/UnAssignAppointment", request, "bearer", token.Token);

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

