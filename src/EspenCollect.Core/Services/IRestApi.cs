namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EspenCollect.Core;

    public interface IRestApi
    {
        Task<IEnumerable<MetabaseCollection>> GetAllCollection();

        Task<IEnumerable<CollectionItem>> GetAllCollectionItem(string collectionId);
    }
}
