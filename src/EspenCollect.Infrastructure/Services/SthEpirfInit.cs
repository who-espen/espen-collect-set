namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Microsoft.Office.Interop.Excel;
    using Excel = Microsoft.Office.Interop.Excel;

    public class SthEpirfInit : ISthEpirfInit
    {
        private readonly IRestApi _restApi;

        public SthEpirfInit(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public Task DispatchToEpirfSheet(List<string> ids, Workbook epirfWorkBook)
        {
            var metabaseCard = new MetabaseCardEpirfQuery();

            var sthSheet = epirfWorkBook.Worksheets.get_Item("STH") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(sthSheet, metabaseCard);

            return Task.CompletedTask;
        }

        public Task DispatchToEpirfSheetToEdit(List<string> ids, Workbook epirfWorkBook)
        {
            var metabaseCard = new MetabaseCardEpirfQuery();

            var sthSheet = epirfWorkBook.Worksheets.get_Item("STH") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(sthSheet, metabaseCard);
            FillOtherEpirfFile(epirfWorkBook, metabaseCard, sthSheet);

            return Task.CompletedTask;
        }

        private void FillEpirfFile(Worksheet lfSheet, MetabaseCardEpirfQuery rowsData)
        {
            var sthEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirSthfModel(rowsData);

            lfSheet.Unprotect("MDA");

            var c = lfSheet.Columns["I"] as Range;
            c.NumberFormat = "@";

            lfSheet.Range["A8:AB8"].Copy(lfSheet.Range[$"A8:AB{7 + sthEpirfData.Count()}"]);

            for (var i = 0; i < sthEpirfData.Count(); i++)
            {
                lfSheet.Cells[i + 8, "A"] = sthEpirfData[i].SurveyType;
                lfSheet.Cells[i + 8, "B"] = sthEpirfData[i].IuName;
                lfSheet.Cells[i + 8, "C"] = sthEpirfData[i].CommunityName;
                lfSheet.Cells[i + 8, "D"] = sthEpirfData[i].NumberOfRoundsPC;
                lfSheet.Cells[i + 8, "E"] = sthEpirfData[i].Month;
                lfSheet.Cells[i + 8, "F"] = sthEpirfData[i].Year;
                lfSheet.Cells[i + 8, "G"] = sthEpirfData[i].Latitude;
                lfSheet.Cells[i + 8, "H"] = sthEpirfData[i].Longitude;
                lfSheet.Cells[i + 8, "I"] = sthEpirfData[i].AgeGroupSurveyed;
                lfSheet.Cells[i + 8, "J"] = sthEpirfData[i].DiagnosticTest;
                lfSheet.Cells[i + 8, "K"] = sthEpirfData[i].AscarisNumberOfPeopleExamined;
                lfSheet.Cells[i + 8, "L"] = sthEpirfData[i].AscarisNumberOfPeoplePositive;
                lfSheet.Cells[i + 8, "M"] = sthEpirfData[i].AscarisPercentagePositive;
                lfSheet.Cells[i + 8, "N"] = sthEpirfData[i].AscarisPercentageHeavy;
                lfSheet.Cells[i + 8, "O"] = sthEpirfData[i].AscarisPercentageModerate;
                lfSheet.Cells[i + 8, "P"] = sthEpirfData[i].HookwormNumberOfPeopleExamined;
                lfSheet.Cells[i + 8, "Q"] = sthEpirfData[i].HookwormNumberOfPeoplePositive;
                lfSheet.Cells[i + 8, "R"] = sthEpirfData[i].HookwormPercentagePositive;
                lfSheet.Cells[i + 8, "S"] = sthEpirfData[i].HookwormPercentageHeavy;
                lfSheet.Cells[i + 8, "T"] = sthEpirfData[i].HookwormPercentageModerate;
                lfSheet.Cells[i + 8, "U"] = sthEpirfData[i].TrichurisNumberOfPeopleExamined;
                lfSheet.Cells[i + 8, "V"] = sthEpirfData[i].TrichurisNumberOfPeoplePositive;
                lfSheet.Cells[i + 8, "W"] = sthEpirfData[i].TrichurisPercentagePositive;
                lfSheet.Cells[i + 8, "X"] = sthEpirfData[i].TrichurisPercentageHeavy;
                lfSheet.Cells[i + 8, "Y"] = sthEpirfData[i].TrichurisPercentageModerate;
                lfSheet.Cells[i + 8, "Z"] = sthEpirfData[i].SthExamined;
                lfSheet.Cells[i + 8, "AA"] = sthEpirfData[i].SthPositive;
                lfSheet.Cells[i + 8, "AB"] = sthEpirfData[i].SthPercentagePositve;
            }

            lfSheet.Protect();
        }
        private void FillOtherEpirfFile(Workbook epirfWorkBook, MetabaseCardEpirfQuery rowsData, Excel.Worksheet sthSheet)
        {
            sthSheet.Unprotect("MDA");
            epirfWorkBook.Unprotect("MDA");
            var newSthSheet = (Worksheet)epirfWorkBook.Worksheets.Add(After: epirfWorkBook.Sheets[epirfWorkBook.Sheets.Count]);
            newSthSheet.Name = "STH Raw";

            sthSheet.Range["A6:AS6"].Copy();
            newSthSheet.Range["A1:AB1"].PasteSpecial(XlPasteType.xlPasteValues);
            //sthSheet.Range[$"A8:AB{rowsData.Data.Rows.Count()+8}"].Copy();
            //newSthSheet.Range[$"A2:AB{rowsData.Data.Rows.Count()}"].PasteSpecial(XlPasteType.xlPasteValues);

            for (var i = 0; i < rowsData.Data.Rows.Count(); i++)
            {
                newSthSheet.Range[$"A{i + 2}"].Formula = $"=STH!A{i + 8}";
                newSthSheet.Range[$"B{i + 2}"].Formula = $"=STH!B{i + 8}";
                newSthSheet.Range[$"C{i + 2}"].Formula = $"=STH!C{i + 8}";
                newSthSheet.Range[$"D{i + 2}"].Formula = $"=STH!D{i + 8}";
                newSthSheet.Range[$"E{i + 2}"].Formula = $"=STH!E{i + 8}";
                newSthSheet.Range[$"F{i + 2}"].Formula = $"=STH!F{i + 8}";
                newSthSheet.Range[$"G{i + 2}"].Formula = $"=STH!G{i + 8}";
                newSthSheet.Range[$"H{i + 2}"].Formula = $"=STH!H{i + 8}";
                newSthSheet.Range[$"I{i + 2}"].Formula = $"=STH!I{i + 8}";
                newSthSheet.Range[$"J{i + 2}"].Formula = $"=STH!J{i + 8}";
                newSthSheet.Range[$"K{i + 2}"].Formula = $"=STH!K{i + 8}";
                newSthSheet.Range[$"L{i + 2}"].Formula = $"=STH!L{i + 8}";
                newSthSheet.Range[$"M{i + 2}"].Formula = $"=STH!M{i + 8}";
                newSthSheet.Range[$"N{i + 2}"].Formula = $"=STH!N{i + 8}";
                newSthSheet.Range[$"O{i + 2}"].Formula = $"=STH!O{i + 8}";
                newSthSheet.Range[$"P{i + 2}"].Formula = $"=STH!P{i + 8}";
                newSthSheet.Range[$"Q{i + 2}"].Formula = $"=STH!Q{i + 8}";
                newSthSheet.Range[$"R{i + 2}"].Formula = $"=STH!R{i + 8}";
                newSthSheet.Range[$"S{i + 2}"].Formula = $"=STH!S{i + 8}";
                newSthSheet.Range[$"T{i + 2}"].Formula = $"=STH!T{i + 8}";
                newSthSheet.Range[$"U{i + 2}"].Formula = $"=STH!U{i + 8}";
                newSthSheet.Range[$"V{i + 2}"].Formula = $"=STH!V{i + 8}";
                newSthSheet.Range[$"W{i + 2}"].Formula = $"=STH!W{i + 8}";
                newSthSheet.Range[$"X{i + 2}"].Formula = $"=STH!X{i + 8}";
                newSthSheet.Range[$"Y{i + 2}"].Formula = $"=STH!Y{i + 8}";
                newSthSheet.Range[$"Z{i + 2}"].Formula = $"=STH!Z{i + 8}";
                newSthSheet.Range[$"AA{i + 2}"].Formula = $"=STH!AA{i + 8}";
                newSthSheet.Range[$"AB{i + 2}"].Formula = $"=STH!AB{i + 8}";
            }
        }
    }
}
