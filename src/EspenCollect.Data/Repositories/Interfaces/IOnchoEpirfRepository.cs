namespace EspenCollect.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EspenCollect.Data.Models;

    public interface IOnchoEpirfRepository
    {
        Task<IEnumerable<OnchoEpirf>> GetAllEpirfOnchoAsync();
    }
}
