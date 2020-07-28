
namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EspenCollect.Core;

    public interface IEpirfGenerator
    {
        Task GenerateEpirfAsync(IList<EpirfSpec> epirfSpecs, string path);
    }
}
