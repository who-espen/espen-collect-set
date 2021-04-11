
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

            return Task.CompletedTask;
        }

        public Task DispatchToEpirfSheetToEdit(List<string> ids, Workbook epirfWorkBook)
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


            //for (var i = 0; i < lfEpirfData.Count(); i++)
            //{
            //    lfSheet.Cells[i + firstCell, "A"] = lfEpirfData[i].TypeOfSurvey;
            //    //lfSheet.Cells[i + firstCell, "B"] = lfEpirfData[i].EuName;

            //    lfSheet.Range[$"{i + firstCell}B"].Formula = "=1+1";

            //    lfSheet.Cells[i + firstCell, "C"] = lfEpirfData[i].IuName;
            //    lfSheet.Cells[i + firstCell, "D"] = lfEpirfData[i].SiteName;
            //    lfSheet.Cells[i + firstCell, "E"] = lfEpirfData[i].Month;
            //    lfSheet.Cells[i + firstCell, "F"] = lfEpirfData[i].Year;
            //    lfSheet.Cells[i + firstCell, "G"] = lfEpirfData[i].Latitude;
            //    lfSheet.Cells[i + firstCell, "H"] = lfEpirfData[i].Longitude;
            //    lfSheet.Cells[i + firstCell, "I"] = lfEpirfData[i].DateFirsrPcRound;
            //    lfSheet.Cells[i + firstCell, "J"] = lfEpirfData[i].NumberOfPcRoundDeliveres;
            //    lfSheet.Cells[i + firstCell, "K"] = lfEpirfData[i].DiagnosticTest;
            //    lfSheet.Cells[i + firstCell, "L"] = lfEpirfData[i].AgeGroupSurveyedMinMax;
            //    lfSheet.Cells[i + firstCell, "M"] = lfEpirfData[i].SurveySite;
            //    lfSheet.Cells[i + firstCell, "N"] = lfEpirfData[i].SurveyType;
            //    lfSheet.Cells[i + firstCell, "O"] = lfEpirfData[i].TargetSampleSize;
            //    lfSheet.Cells[i + firstCell, "P"] = lfEpirfData[i].NumberOfPeopleExamined;
            //    lfSheet.Cells[i + firstCell, "Q"] = lfEpirfData[i].NumberOfPeoplePositive;
            //    lfSheet.Cells[i + firstCell, "R"] = lfEpirfData[i].PrecentagePositive;
            //    lfSheet.Cells[i + firstCell, "S"] = lfEpirfData[i].NumberOfInvalidTests;
            //    lfSheet.Cells[i + firstCell, "T"] = lfEpirfData[i].Decision;
            //    lfSheet.Cells[i + firstCell, "U"] = lfEpirfData[i].LymphoedemaTotalNumberOfPatients;
            //    lfSheet.Cells[i + firstCell, "V"] = lfEpirfData[i].LymphoedemaMethodOfPatientEstimation;
            //    lfSheet.Cells[i + firstCell, "W"] = lfEpirfData[i].LymphoedemaDateOfPatientEstimation;
            //    lfSheet.Cells[i + firstCell, "X"] = lfEpirfData[i].LymphoedemaNbrHealthFacilities;
            //    lfSheet.Cells[i + firstCell, "Y"] = lfEpirfData[i].HydrocoeleTotalNumberOfPatients;
            //    lfSheet.Cells[i + firstCell, "Z"] = lfEpirfData[i].HydrocoeleMethodOfPatientEstimation;
            //    lfSheet.Cells[i + firstCell, "AA"] = lfEpirfData[i].HydrocoeleDateOfPatientEstimation;
            //    lfSheet.Cells[i + firstCell, "AB"] = lfEpirfData[i].HydrocoeleNumberOfHealthFacilities;
            //    lfSheet.Cells[i + firstCell, "AC"] = lfEpirfData[i].Comments;
            //}
            lfSheet.Range["B17"].Formula = "=1+1";
            lfSheet.Protect();
        }

        private void FillOtherEpirfFile(Workbook epirfWorkBook, MetabaseCardEpirfQuery rowsData, Excel.Worksheet lfSheet)
        {
            lfSheet.Unprotect("MDA");
            epirfWorkBook.Unprotect("MDA");
            var newLfSheet = (Worksheet) epirfWorkBook.Worksheets.Add(After: epirfWorkBook.Sheets[epirfWorkBook.Sheets.Count]);
            newLfSheet.Name = "LF Raw";

            lfSheet.Range["A15:AC15"].Copy();
            newLfSheet.Range["A1:AC1"].PasteSpecial(XlPasteType.xlPasteValues);
            lfSheet.Range[$"A17:AC{rowsData.Data.Rows.Count()+17}"].Copy();
            newLfSheet.Range[$"A2:AC{rowsData.Data.Rows.Count()}"].PasteSpecial(XlPasteType.xlPasteValues);

        }

        //private void FillOtherEpirfFile2(Workbook epirfWorkBook, MetabaseCardEpirfQuery rowsData, Excel.Worksheet lfSheet)
        //{
        //    lfSheet.Unprotect("MDA");
        //    epirfWorkBook.Unprotect("MDA");
        //    var newLfSheet = (Worksheet)epirfWorkBook.Worksheets.Add(After: epirfWorkBook.Sheets[epirfWorkBook.Sheets.Count]);
        //    newLfSheet.Name = "LF Raw";

        //    for (var i = 0; i < lfEpirfData.Count(); i++)
        //    {
        //        lfSheet.Cells[i + 17, "A"].Formula = "";
        //        lfSheet.Cells[i + 17, "B"] = lfEpirfData[i].EuName;
        //        lfSheet.Cells[i + 17, "C"] = lfEpirfData[i].IuName;
        //        lfSheet.Cells[i + 17, "D"] = lfEpirfData[i].SiteName;
        //        lfSheet.Cells[i + 17, "E"] = lfEpirfData[i].Month;
        //        lfSheet.Cells[i + 17, "F"] = lfEpirfData[i].Year;
        //        lfSheet.Cells[i + 17, "G"] = lfEpirfData[i].Latitude;
        //        lfSheet.Cells[i + 17, "H"] = lfEpirfData[i].Longitude;
        //        lfSheet.Cells[i + 17, "I"] = lfEpirfData[i].DateFirsrPcRound;
        //        lfSheet.Cells[i + 17, "J"] = lfEpirfData[i].NumberOfPcRoundDeliveres;
        //        lfSheet.Cells[i + 17, "K"] = lfEpirfData[i].DiagnosticTest;
        //        lfSheet.Cells[i + 17, "L"] = lfEpirfData[i].AgeGroupSurveyedMinMax;
        //        lfSheet.Cells[i + 17, "M"] = lfEpirfData[i].SurveySite;
        //        lfSheet.Cells[i + 17, "N"] = lfEpirfData[i].SurveyType;
        //        lfSheet.Cells[i + 17, "O"] = lfEpirfData[i].TargetSampleSize;
        //        lfSheet.Cells[i + 17, "P"] = lfEpirfData[i].NumberOfPeopleExamined;
        //        lfSheet.Cells[i + 17, "Q"] = lfEpirfData[i].NumberOfPeoplePositive;
        //        lfSheet.Cells[i + 17, "R"] = lfEpirfData[i].PrecentagePositive;
        //        lfSheet.Cells[i + 17, "S"] = lfEpirfData[i].NumberOfInvalidTests;
        //        lfSheet.Cells[i + 17, "T"] = lfEpirfData[i].Decision;
        //        lfSheet.Cells[i + 17, "U"] = lfEpirfData[i].LymphoedemaTotalNumberOfPatients;
        //        lfSheet.Cells[i + 17, "V"] = lfEpirfData[i].LymphoedemaMethodOfPatientEstimation;
        //        lfSheet.Cells[i + 17, "W"] = lfEpirfData[i].LymphoedemaDateOfPatientEstimation;
        //        lfSheet.Cells[i + 17, "X"] = lfEpirfData[i].LymphoedemaNbrHealthFacilities;
        //        lfSheet.Cells[i + 17, "Y"] = lfEpirfData[i].HydrocoeleTotalNumberOfPatients;
        //        lfSheet.Cells[i + 17, "Z"] = lfEpirfData[i].HydrocoeleMethodOfPatientEstimation;
        //        lfSheet.Cells[i + 17, "AA"] = lfEpirfData[i].HydrocoeleDateOfPatientEstimation;
        //        lfSheet.Cells[i + 17, "AB"] = lfEpirfData[i].HydrocoeleNumberOfHealthFacilities;
        //        lfSheet.Cells[i + 17, "AC"] = lfEpirfData[i].Comments;
        //    }

        //    lfSheet.Protect();
        //}
        //https://stackoverflow.com/questions/8878896/adding-formula-to-excel-with-c-sharp-making-the-formula-shown

    }
}
