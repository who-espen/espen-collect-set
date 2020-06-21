namespace EspenCollect.Services
{
    using System.Threading.Tasks;

    public interface IOnchoEpirfGenerator
    {
        Task GenerateOnchoEpirfAsync(string id);
    }
}
