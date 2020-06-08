namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using RestSharp;
    using RestSharp.Serializers.NewtonsoftJson;

    public class RestApi: IRestApi
    {
        private readonly RestClient _restClient;

        public RestApi()
        {

            var jsonNetSettings = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ContractResolver  = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },

                DefaultValueHandling = DefaultValueHandling.Include,
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };

            _restClient = new RestClient("https://oem.securedatakit.com/api");
            _restClient.AddDefaultHeader("X-Metabase-Session", "08be678f-8838-4f56-a8ac-183afa5f475b");
            _restClient.UseNewtonsoftJson(jsonNetSettings);
            _restClient.FailOnDeserializationError = true;
            _restClient.ThrowOnAnyError = true;
            _restClient.ThrowOnDeserializationError = true;
        }

        public async Task<IEnumerable<MetabaseCollection>> GetAllCollection()
        {
            try
            {
                var request = new RestRequest("collection", DataFormat.Json);

                var collections = await _restClient.GetAsync<List<MetabaseCollection>>(request).ConfigureAwait(false);

                var results = collections.Where(c => !c.Archived && c.PersonalOwnerId == null && c.Id != "root");

                return results;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CollectionItem>> GetAllCollectionItem(string collectionId)
        {
            try
            {
                var request = new RestRequest($"collection/{collectionId}/items", DataFormat.Json);

                var response = _restClient.Get(request);

                var items = await _restClient.GetAsync<List<CollectionItem>>(request).ConfigureAwait(false);

                var results = items.Where(i => i.Model == "card" && i.Name.ToUpper().Contains("EPIRF"));

                return results;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}
