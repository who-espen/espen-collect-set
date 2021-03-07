namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGenerator : IEpirfGenerator
    {
        private readonly IOnchoEpirfInit _onchoEpirfGenerator;
        private readonly ILfEpirfInit _lfEpirfInit;
        private readonly ISthEpirfInit _sthEpirfInit;
        private readonly ISchEpirfInit _schEpirfInit;

        public EpirfGenerator(IOnchoEpirfInit onchoEpirfGenerator, ILfEpirfInit lfEpirfInit, ISthEpirfInit sthEpirfInit, ISchEpirfInit schEpirfInit)
        {
            _onchoEpirfGenerator = onchoEpirfGenerator;
            _lfEpirfInit = lfEpirfInit;
            _sthEpirfInit = sthEpirfInit;
            _schEpirfInit = schEpirfInit;
        }

        public async Task<bool> GenerateEpirfAsync(IList<EpirfSpec> epirfSpecs, string path)
        {
            var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var EpirfCardsIds = new EpirfCardsIds();

            foreach (var e in epirfSpecs)
            {
                if (e.Name.ToUpper().Contains("ONCHO"))
                {
                    EpirfCardsIds.OnchoIds.Add(e.Id.ToString());
                }
                else if (e.Name.ToUpper().Contains("LF"))
                {
                    EpirfCardsIds.LfIds.Add(e.Id.ToString());
                }
                else if (e.Name.ToUpper().Contains("STH"))
                {
                    EpirfCardsIds.SthIds.Add(e.Id.ToString());
                }
                else if (e.Name.ToUpper().Contains("SCH") || e.Name.ToUpper().Contains("SCHISTO"))
                {
                    EpirfCardsIds.SchIds.Add(e.Id.ToString());
                }

                if (EpirfCardsIds.LfIds.Any())
                {
                    var lfSheet = epirfWorkBook.Worksheets.get_Item("LF") as Excel.Worksheet;

                    await _lfEpirfInit.DispatchToEpirfSheet(EpirfCardsIds.LfIds, lfSheet).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.OnchoIds.Any())
                {
                    var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

                    await _onchoEpirfGenerator.DispatchToEpirfSheet(EpirfCardsIds.OnchoIds, onchoSheet).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.SthIds.Any())
                {
                    var sthSheet = epirfWorkBook.Worksheets.get_Item("STH") as Excel.Worksheet;

                    await _sthEpirfInit.DispatchToEpirfSheet(EpirfCardsIds.SthIds, sthSheet).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.SchIds.Any())
                {
                    var sthSheet = epirfWorkBook.Worksheets.get_Item("SCH") as Excel.Worksheet;

                    await _schEpirfInit.DispatchToEpirfSheet(EpirfCardsIds.SchIds, sthSheet).ConfigureAwait(false);
                }

            }


            excelApp.Visible = true;
            epirfWorkBook.SaveAs(path);
            epirfWorkBook.Close(true);
            excelApp.Quit();

            return await Task.FromResult(true);
        }

    }
}
