using System;

namespace OnSpa.Common.Responses
{
    public class AppointmentResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public UserResponse User { get; set; }

        public ServiceResponse Service { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();
    }
}
