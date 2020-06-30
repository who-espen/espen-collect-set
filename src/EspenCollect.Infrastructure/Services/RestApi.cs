namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
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
            _restClient.AddDefaultHeader("X-Metabase-Session", "677e9caf-1828-4e31-9034-eb632e4dd833");
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

                if(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted
                     || response.StatusCode == HttpStatusCode.Created)
                {
                    var items = JsonConvert.DeserializeObject<List<CollectionItem>>(response.Content);

                    var results = items.Where(i => i.Model == "card" && i.Name.ToUpper().Contains("EPIRF"));

                    return await Task.FromResult(results);
                }

                //return results;
                return await Task.FromResult(new List<CollectionItem>().AsEnumerable());
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

        public async Task<MetabaseCardEpirfQuery> GetEpirfCard(string cardId)
        {
            try
            {
                var request = new RestRequest($"card/{cardId}/query", DataFormat.Json);

                var response = _restClient.Post(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted
                     || response.StatusCode == HttpStatusCode.Created)
                {
                    var results = JsonConvert.DeserializeObject<MetabaseCardEpirfQuery>(response.Content);

                    return await Task.FromResult(results);
                }

                //return results;
                return await Task.FromResult(new MetabaseCardEpirfQuery());
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
    }
}
