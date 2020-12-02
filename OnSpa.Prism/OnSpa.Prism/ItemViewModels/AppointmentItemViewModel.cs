using Newtonsoft.Json;
using OnSpa.Common.Helpers;
using OnSpa.Common.Responses;
using Prism.Commands;
using Prism.Navigation;

namespace OnSpa.Prism.ItemViewModels
{
    public class AppointmentItemViewModel : AppointmentResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectAppointmentCommand;

        public AppointmentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectAppointmentCommand => _selectAppointmentCommand ?? (_selectAppointmentCommand = new DelegateCommand(SelectAppointment));

        private async void SelectAppointment()
        {
            var user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            if (!IsActive && User.Id != user.Id)
            {
                return;
            }

            var parameters = new NavigationParameters
            {
                { "Appointment", this }
            };

            await _navigationService.NavigateAsync("AssignModifyAppointmentPage", parameters);
        }
    }
}