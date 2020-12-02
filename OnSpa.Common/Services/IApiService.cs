using OnSpa.Common.Models;
using System.IO;
using System.Threading.Tasks;
using OnSpa.Common.Responses;
using OnSpa.Common.Request;

namespace OnSpa.Common.Services
{
    public interface IApiService
    {
        Task<RandomUsers> GetRandomUser(string urlBase, string servicePrefix);

        Task<Stream> GetPictureAsync(string urlBase, string servicePrefix);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, FacebookProfile request);

        Task<Response> ModifyUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest, string token);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> GetAgendaForCustomer(string urlBase, string servicePrefix, string controller, string email, string tokenType, string accessToken);

        Task<Response> PostAsync<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           T model,
           string tokenType,
           string accessToken);
    }
}
