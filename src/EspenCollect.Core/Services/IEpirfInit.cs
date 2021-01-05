namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Office.Interop.Excel;

    public interface IEpirfInit
    {
        Task DispatchToEpirfSheet(string id, Worksheet epirfSheet);


        Task DispatchToEpirfSheet2(List<string> id, Worksheet epirfSheet);
    }
}
