namespace EspenCollect.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EspenCollect.Data.Models;
    using Insight.Database;

    public class OnchoEpirfRepository : RepositoryBase, IOnchoEpirfRepository
    {
        public async Task<IEnumerable<OnchoEpirf>> GetAllEpirfOnchoAsync()
        {
            var epirfs = await Connection.QuerySqlAsync<OnchoEpirf>("select * from epirf_code").ConfigureAwait(false);

            return epirfs;
        }
    }
}
