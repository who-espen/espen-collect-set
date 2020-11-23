namespace EspenCollect.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IAuthenticationService
    {
        Task Authenticate(string username, string password, Action<string> successCallback,
            Action<string> failureCallback);
    }
}
