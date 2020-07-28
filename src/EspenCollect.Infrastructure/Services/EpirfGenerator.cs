namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGenerator : IEpirfGenerator
    {
        private readonly IOnchoEpirfGenerator _onchoEpirfGenerator;

        public EpirfGenerator(IOnchoEpirfGenerator onchoEpirfGenerator)
        {
            _onchoEpirfGenerator = onchoEpirfGenerator;
        }

        public async Task GenerateEpirfAsync(IList<EpirfSpec> epirfSpecs, string path)
        {
            //var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);
            //var sheet = epirfWorkBook.Worksheets.get_Item(epirfName) as Excel.Worksheet;

            foreach (var e in epirfSpecs)
            {
                if (e.Name.ToUpper().Contains("ONCHO"))
                {
                    var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

                    await _onchoEpirfGenerator.DispatchToOnchoSheet(e.Id.ToString(), onchoSheet).ConfigureAwait(false);
                }
            }


            //fillEpirfFile(sheet, rowsData);

            excelApp.Visible = true;
            epirfWorkBook.SaveAs(path);
            epirfWorkBook.Close(true);
            excelApp.Quit();
        }
    }
}
