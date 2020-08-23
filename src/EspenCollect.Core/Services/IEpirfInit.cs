namespace EspenCollect.Services
{
    using System.Threading.Tasks;
    using Microsoft.Office.Interop.Excel;

    public interface IEpirfInit
    {
        Task DispatchToEpirfSheet(string id, Worksheet epirfSheet);
    }
}
