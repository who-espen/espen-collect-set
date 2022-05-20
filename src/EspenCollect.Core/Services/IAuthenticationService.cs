namespace EspenCollect.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IAuthenticationService
    {
        Task<string> Authenticate(string username, string password);
    }
}
