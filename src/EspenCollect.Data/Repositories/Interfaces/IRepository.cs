namespace EspenCollect.Data.Repositories
{
    using System.Data.SqlClient;
    using Npgsql;

    public interface IRepository
    {
        NpgsqlConnection Connection { get; }
    }
}
