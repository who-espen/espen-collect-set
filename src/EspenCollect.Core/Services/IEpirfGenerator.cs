
namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EspenCollect.Core;

    public interface IEpirfGenerator
    {
        Task<bool> GenerateEpirfAsync(IList<EpirfSpec> epirfSpecs, string path);

        Task<bool> GenerateEpirfForEditAsync(IList<EpirfSpec> epirfSpecs, string path);
    }
}
