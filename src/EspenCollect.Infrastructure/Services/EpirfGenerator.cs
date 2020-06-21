namespace EspenCollect.Services
{
    using System.Threading.Tasks;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGenerator : IEpirfGenerator
    {
        public Task GenerateOnchoEpirfAsync()
        {
            var filePath = @"Resources\WHO_EPIRF_PC.xlsm";
            var excelApp = new Excel.Application();

            excelApp.Visible = false;

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            //prepareEirfFRow(onchoSheet, onchoData.Count);





            throw new System.NotImplementedException();
        }
    }
}
