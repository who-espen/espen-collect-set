namespace EspenCollect.Services
{
    using System.Threading.Tasks;
    using Microsoft.Office.Interop.Excel;
    using Excel = Microsoft.Office.Interop.Excel;

    public interface IOnchoEpirfGenerator
    {
        Task GenerateOnchoEpirfAsync(string id, string path);

        Task DispatchToOnchoSheet(string id, Worksheet onchoSheet);
    }
}
