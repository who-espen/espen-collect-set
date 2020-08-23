namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGenerator : IEpirfGenerator
    {
        private readonly IOnchoEpirfInit _onchoEpirfGenerator;

        public EpirfGenerator(IOnchoEpirfInit onchoEpirfGenerator)
        {
            _onchoEpirfGenerator = onchoEpirfGenerator;
        }

        public async Task GenerateEpirfAsync(IList<EpirfSpec> epirfSpecs, string path)
        {
            var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            foreach (var e in epirfSpecs)
            {
                if (e.Name.ToUpper().Contains("ONCHO"))
                {
                    var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

                    await _onchoEpirfGenerator.DispatchToEpirfSheet(e.Id.ToString(), onchoSheet).ConfigureAwait(false);
                }
                else if (e.Name.ToUpper().Contains("LF"))
                {
                    var onchoSheet = epirfWorkBook.Worksheets.get_Item("LF") as Excel.Worksheet;

                    await _onchoEpirfGenerator.DispatchToEpirfSheet(e.Id.ToString(), onchoSheet).ConfigureAwait(false);
                }
            }


            excelApp.Visible = true;
            epirfWorkBook.SaveAs(path);
            epirfWorkBook.Close(true);
            excelApp.Quit();
        }
    }
}
