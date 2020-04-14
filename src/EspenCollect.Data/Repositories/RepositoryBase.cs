namespace EspenCollect.Data.Repositories
{
    using System.Data.SqlClient;
    using Npgsql;

    public class RepositoryBase: IRepository
    {        
        public NpgsqlConnection Connection { get; } = new NpgsqlConnection(@"User ID=yumba;Password=a3gho84fbco8zHWc;
              Host=sdk-oem.cg9e3y9ijr0z.eu-west-2.rds.amazonaws.com;Port=5432;Database=oem_ghana;Pooling=true;");

    }
}
