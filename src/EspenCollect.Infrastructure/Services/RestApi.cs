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
            _restClient.AddDefaultHeader("X-Metabase-Session", "32e917d9-c75b-464c-bd0f-c652e6a62d9d");
            _restClient.UseNewtonsoftJson(jsonNetSettings);
        }

        public async Task<IEnumerable<MetabaseCollection>> GetAllCollection()
        {
            try
            {
                var request = new RestRequest("collection", DataFormat.Json);

                var collections = await _restClient.GetAsync<List<MetabaseCollection>>(request);

                var results = collections.Where(c => !c.Archived && c.PersonalOwnerId == null && c.Id != "root");

                return results;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}
