namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EspenCollect.Core;

    public interface IRestApi
    {
        Task<SessionType> Authenticate(string username, string password);

        Task<IEnumerable<MetabaseCollection>> GetAllCollection();

        Task<IEnumerable<CollectionItem>> GetAllCollectionItem(string collectionId);

        Task<MetabaseCardEpirfQuery> GetEpirfCard(string cardId);
    }
}
