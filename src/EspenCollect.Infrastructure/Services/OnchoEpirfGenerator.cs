namespace EspenCollect.Services
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Excel = Microsoft.Office.Interop.Excel;

    public class OnchoEpirfGenerator : IOnchoEpirfGenerator
    {
        private readonly IRestApi _restApi;

        public OnchoEpirfGenerator(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public async Task GenerateOnchoEpirfAsync(string id)
        {
            //var filePath2 = @"Resources\WHO_EPIRF_PC.xlsm";
            var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            var onchoData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            prepareEirfFRow(onchoSheet, onchoData.Data.Rows.Count);





            throw new System.NotImplementedException();
        }

        private void prepareEirfFRow(Excel.Worksheet onchoSheet, int lenghth)
        {
            onchoSheet.Unprotect();

            onchoSheet.Range["A8:AE8"].Copy(onchoSheet.Range[$"A8:AE{7 + lenghth}"]);

            onchoSheet.Protect();
        }
    }
}
