
namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Microsoft.Office.Interop.Excel;
    using Excel = Microsoft.Office.Interop.Excel;

    public class LfEpirfInit : ILfEpirfInit
    {
        private readonly IRestApi _restApi;

        public LfEpirfInit(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public Task DispatchToEpirfSheet(List<string> ids, Workbook epirfWorkBook)
        {
            var metabaseCard = new MetabaseCardEpirfQuery();
            var lfSheet = epirfWorkBook.Worksheets.get_Item("LF") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(lfSheet, metabaseCard, 17);
            FillOtherEpirfFile(epirfWorkBook, metabaseCard, lfSheet);

            return Task.CompletedTask;
        }

        private void FillEpirfFile(Worksheet lfSheet, MetabaseCardEpirfQuery rowsData, int firstCell = 2)
        {
            var lfEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirfLfModel(rowsData);

            lfSheet.Unprotect("MDA");

            var c = lfSheet.Columns["L"] as Range;
            c.NumberFormat = "@";

            lfSheet.Range[$"A{firstCell}:AC{firstCell}"].Copy(lfSheet.Range[$"A{firstCell}:AC{firstCell - 1 + lfEpirfData.Count()}"]);

            for (var i = 0; i < lfEpirfData.Count(); i++)
            {
                lfSheet.Cells[i + firstCell, "A"] = lfEpirfData[i].TypeOfSurvey;
                lfSheet.Cells[i + firstCell, "B"] = lfEpirfData[i].EuName;
                lfSheet.Cells[i + firstCell, "C"] = lfEpirfData[i].IuName;
                lfSheet.Cells[i + firstCell, "D"] = lfEpirfData[i].SiteName;
                lfSheet.Cells[i + firstCell, "E"] = lfEpirfData[i].Month;
                lfSheet.Cells[i + firstCell, "F"] = lfEpirfData[i].Year;
                lfSheet.Cells[i + firstCell, "G"] = lfEpirfData[i].Latitude;
                lfSheet.Cells[i + firstCell, "H"] = lfEpirfData[i].Longitude;
                lfSheet.Cells[i + firstCell, "I"] = lfEpirfData[i].DateFirsrPcRound;
                lfSheet.Cells[i + firstCell, "J"] = lfEpirfData[i].NumberOfPcRoundDeliveres;
                lfSheet.Cells[i + firstCell, "K"] = lfEpirfData[i].DiagnosticTest;
                lfSheet.Cells[i + firstCell, "L"] = lfEpirfData[i].AgeGroupSurveyedMinMax;
                lfSheet.Cells[i + firstCell, "M"] = lfEpirfData[i].SurveySite;
                lfSheet.Cells[i + firstCell, "N"] = lfEpirfData[i].SurveyType;
                lfSheet.Cells[i + firstCell, "O"] = lfEpirfData[i].TargetSampleSize;
                lfSheet.Cells[i + firstCell, "P"] = lfEpirfData[i].NumberOfPeopleExamined;
                lfSheet.Cells[i + firstCell, "Q"] = lfEpirfData[i].NumberOfPeoplePositive;
                lfSheet.Cells[i + firstCell, "R"] = lfEpirfData[i].PrecentagePositive;
                lfSheet.Cells[i + firstCell, "S"] = lfEpirfData[i].NumberOfInvalidTests;
                lfSheet.Cells[i + firstCell, "T"] = lfEpirfData[i].Decision;
                lfSheet.Cells[i + firstCell, "U"] = lfEpirfData[i].LymphoedemaTotalNumberOfPatients;
                lfSheet.Cells[i + firstCell, "V"] = lfEpirfData[i].LymphoedemaMethodOfPatientEstimation;
                lfSheet.Cells[i + firstCell, "W"] = lfEpirfData[i].LymphoedemaDateOfPatientEstimation;
                lfSheet.Cells[i + firstCell, "X"] = lfEpirfData[i].LymphoedemaNbrHealthFacilities;
                lfSheet.Cells[i + firstCell, "Y"] = lfEpirfData[i].HydrocoeleTotalNumberOfPatients;
                lfSheet.Cells[i + firstCell, "Z"] = lfEpirfData[i].HydrocoeleMethodOfPatientEstimation;
                lfSheet.Cells[i + firstCell, "AA"] = lfEpirfData[i].HydrocoeleDateOfPatientEstimation;
                lfSheet.Cells[i + firstCell, "AB"] = lfEpirfData[i].HydrocoeleNumberOfHealthFacilities;
                lfSheet.Cells[i + firstCell, "AC"] = lfEpirfData[i].Comments;
            }

            lfSheet.Protect();
        }

        private void FillOtherEpirfFile(Workbook epirfWorkBook, MetabaseCardEpirfQuery rowsData, Excel.Worksheet lfSheet)
        {
            epirfWorkBook.Unprotect("MDA");
            var newLfSheet = (Worksheet) epirfWorkBook.Worksheets.Add(After: epirfWorkBook.Sheets[epirfWorkBook.Sheets.Count]);
            newLfSheet.Name = "LF Raw";

            lfSheet.Range["A15:AC15"].Copy();
            newLfSheet.Range["A1:AC1"].PasteSpecial(XlPasteType.xlPasteValues);

            FillEpirfFile(newLfSheet, rowsData);

            epirfWorkBook.Protect("MDA");
        }

    }
}
