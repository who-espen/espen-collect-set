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
                    await _lfEpirfInit.DispatchToEpirfSheet(EpirfCardsIds.LfIds, epirfWorkBook).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.OnchoIds.Any())
                {
                    await _onchoEpirfGenerator.DispatchToEpirfSheet(EpirfCardsIds.OnchoIds, epirfWorkBook).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.SthIds.Any())
                {
                    await _sthEpirfInit.DispatchToEpirfSheet(EpirfCardsIds.SthIds, epirfWorkBook).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.SchIds.Any())
                {
                    await _schEpirfInit.DispatchToEpirfSheet(EpirfCardsIds.SchIds, epirfWorkBook).ConfigureAwait(false);
                }

            }


            excelApp.Visible = true;
            epirfWorkBook.SaveAs(path);
            epirfWorkBook.Close(true);
            excelApp.Quit();

            return await Task.FromResult(true);
        }

        public async Task<bool> GenerateEpirfForEditAsync(IList<EpirfSpec> epirfSpecs, string path)
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
                    await _lfEpirfInit.DispatchToEpirfSheetToEdit(EpirfCardsIds.LfIds, epirfWorkBook).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.OnchoIds.Any())
                {
                    await _onchoEpirfGenerator.DispatchToEpirfSheetToEdit(EpirfCardsIds.OnchoIds, epirfWorkBook).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.SthIds.Any())
                {
                    await _sthEpirfInit.DispatchToEpirfSheetToEdit(EpirfCardsIds.SthIds, epirfWorkBook).ConfigureAwait(false);
                }
                else if (EpirfCardsIds.SchIds.Any())
                {
                    await _schEpirfInit.DispatchToEpirfSheetToEdit(EpirfCardsIds.SchIds, epirfWorkBook).ConfigureAwait(false);
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
