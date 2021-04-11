namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Microsoft.Office.Interop.Excel;
    using Excel = Microsoft.Office.Interop.Excel;

    public class SchEpirfInit : ISchEpirfInit
    {
        private readonly IRestApi _restApi;

        public SchEpirfInit(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public Task DispatchToEpirfSheet(List<string> ids, Workbook epirfWorkBook)
        {
            var metabaseCard = new MetabaseCardEpirfQuery();

            var schSheet = epirfWorkBook.Worksheets.get_Item("SCH") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(schSheet, metabaseCard);

            return Task.CompletedTask;
        }

        public Task DispatchToEpirfSheetToEdit(List<string> ids, Workbook epirfWorkBook)
        {

            var metabaseCard = new MetabaseCardEpirfQuery();

            var schSheet = epirfWorkBook.Worksheets.get_Item("SCH") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(schSheet, metabaseCard);
            FillOtherEpirfFile(epirfWorkBook, metabaseCard, schSheet);

            return Task.CompletedTask;
        }

        private void FillEpirfFile(Worksheet schSheet, MetabaseCardEpirfQuery rowsData)
        {
            var sthEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirSchfModel(rowsData);

            schSheet.Unprotect("MDA");

            schSheet.Range["A8:S8"].Copy(schSheet.Range[$"A8:S{7 + sthEpirfData.Count}"]);

            var c = schSheet.Columns["H"] as Range;
            c.NumberFormat = "@";

            for (var i = 0; i < sthEpirfData.Count; i++)
            {
                schSheet.Cells[i + 8, "A"] = sthEpirfData[i].SurveyType;
                schSheet.Cells[i + 8, "B"] = sthEpirfData[i].IuName;
                schSheet.Cells[i + 8, "C"] = sthEpirfData[i].SiteName;
                schSheet.Cells[i + 8, "D"] = sthEpirfData[i].Month;
                schSheet.Cells[i + 8, "E"] = sthEpirfData[i].Year;
                schSheet.Cells[i + 8, "F"] = sthEpirfData[i].Latitude;
                schSheet.Cells[i + 8, "G"] = sthEpirfData[i].Longitude;
                schSheet.Cells[i + 8, "H"] = sthEpirfData[i].AgeGroup;
                schSheet.Cells[i + 8, "I"] = sthEpirfData[i].DiagnosticTest;
                schSheet.Cells[i + 8, "J"] = sthEpirfData[i].UrinaryNumberOfPeopleExamined;
                schSheet.Cells[i + 8, "K"] = sthEpirfData[i].UrinaryNumberofPositive;
                schSheet.Cells[i + 8, "L"] = sthEpirfData[i].UrinaryPercentageOfPositive;
                schSheet.Cells[i + 8, "M"] = sthEpirfData[i].UrinaryPercentageHeavy;
                schSheet.Cells[i + 8, "N"] = sthEpirfData[i].UrinaryPercentageLow;
                schSheet.Cells[i + 8, "O"] = sthEpirfData[i].IntestinalNumberOfPeopleExamined;
                schSheet.Cells[i + 8, "P"] = sthEpirfData[i].IntestinalNumberofPositive;
                schSheet.Cells[i + 8, "Q"] = sthEpirfData[i].IntestinalPercentageOfPositive;
                schSheet.Cells[i + 8, "R"] = sthEpirfData[i].IntestinalPercentageHeavy;
                schSheet.Cells[i + 8, "S"] = sthEpirfData[i].IntestinalPercentageModerate;
            }

            schSheet.Protect();
        }

        private void FillOtherEpirfFile(Workbook epirfWorkBook, MetabaseCardEpirfQuery rowsData, Excel.Worksheet schSheet)
        {
            schSheet.Unprotect("MDA");
            epirfWorkBook.Unprotect("MDA");
            var newSchSheet = (Worksheet)epirfWorkBook.Worksheets.Add(After: epirfWorkBook.Sheets[epirfWorkBook.Sheets.Count]);
            newSchSheet.Name = "SCH Raw";

            schSheet.Range["A6:AS6"].Copy();
            newSchSheet.Range["A1:AS1"].PasteSpecial(XlPasteType.xlPasteValues);
            schSheet.Range[$"A8:AS{rowsData.Data.Rows.Count()+8}"].Copy();
            newSchSheet.Range[$"A2:AS{rowsData.Data.Rows.Count()}"].PasteSpecial(XlPasteType.xlPasteValues);
        }
    }
}
