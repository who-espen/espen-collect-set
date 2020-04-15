namespace EspenCollect.Data.Services
{
    using System.Threading.Tasks;
    using Catel;
    using EspenCollect.Data.Repositories;

    public class EpirfGenerator : IEpirfGenerator
    {
        private readonly IOnchoEpirfRepository _onchoEpirfRepository;

        public EpirfGenerator(IOnchoEpirfRepository onchoEpirf)
        {
            Argument.IsNotNull(() => onchoEpirf);

            _onchoEpirfRepository = onchoEpirf;
        }

        public Task GenerateEpirfAsync(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
