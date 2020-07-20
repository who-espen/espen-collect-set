namespace EspenCollect.Services
{
    using System.Threading.Tasks;

    public interface ILfEpirfGenerator
    {
        Task GenerateLfEpirfAsync(string id, string path);
    }
}
