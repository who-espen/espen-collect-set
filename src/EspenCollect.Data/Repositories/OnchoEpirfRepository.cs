namespace EspenCollect.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Catel.Logging;
    using EspenCollect.Data.Models;
    using Insight.Database;

    public class OnchoEpirfRepository : RepositoryBase, IOnchoEpirfRepository
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<OnchoEpirf>> GetAllEpirfOnchoAsync()
        {
            try
            {
                var epirfs = await Connection.QuerySqlAsync<OnchoEpirf>("select * from epirf_code").ConfigureAwait(false);

                return epirfs;
            }
            catch (SqlException e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
