using System.Threading.Tasks;

namespace OnSpa.Web.Helpers
{
    public interface IAppointmentHelper
    {
        Task AddDaysAsync(int days);
    }

}
