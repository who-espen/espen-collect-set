namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Office.Interop.Excel;

    public interface IEpirfInit
    {
        Task DispatchToEpirfSheet(List<string> ids, Workbook epirfWorkBook);

        Task DispatchToEpirfSheetToEdit(List<string> ids, Workbook epirfWorkBook);
    }
}
