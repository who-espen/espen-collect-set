namespace EspenCollect.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGeneratorBase
    {

        private readonly IRestApi _restApi;

        public EpirfGeneratorBase(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public delegate void DelFillEpirfFile(Excel.Worksheet sheet, MetabaseCardEpirfQuery rowsData);

        public async Task GenerateEpirfAsync(string id, string path, string epirfName, DelFillEpirfFile fillEpirfFile)
        {
            var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var sheet = epirfWorkBook.Worksheets.get_Item(epirfName) as Excel.Worksheet;

            fillEpirfFile(sheet, rowsData);

            excelApp.Visible = true;
            epirfWorkBook.SaveAs(path);
            epirfWorkBook.Close(true);
            excelApp.Quit();
        }
    }
}
