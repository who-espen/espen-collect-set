namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Microsoft.Office.Interop.Excel;
    using Excel = Microsoft.Office.Interop.Excel;

    public class OnchoEpirfInit : IOnchoEpirfInit
    {
        private readonly IRestApi _restApi;

        public OnchoEpirfInit(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public Task DispatchToEpirfSheet(List<string> ids, Workbook epirfWorkBook)
        {
            var metabaseCard = new MetabaseCardEpirfQuery();
            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(onchoSheet, metabaseCard, 8);

            return Task.CompletedTask;
        }


        public Task DispatchToEpirfSheetToEdit(List<string> ids, Workbook epirfWorkBook)
        {
            var metabaseCard = new MetabaseCardEpirfQuery();
            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            ids.ForEach(async id =>
            {
                var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

                metabaseCard.RowCount = rowsData.RowCount;
                metabaseCard.Data.Rows.AddRange(rowsData.Data.Rows);
            });

            FillEpirfFile(onchoSheet, metabaseCard, 8);
            FillOtherEpirfFile(epirfWorkBook, metabaseCard, onchoSheet);

            return Task.CompletedTask;
        }

        private void FillEpirfFile(Worksheet onchoSheet, MetabaseCardEpirfQuery rowsData, int firstCell = 2)
        {
            var onchoEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirfOnchoModel(rowsData);

            onchoSheet.Unprotect("MDA");

            onchoSheet.Range[$"A{firstCell}:AE{firstCell}"].Copy(onchoSheet.Range[$"A{firstCell}:AE{firstCell + onchoEpirfData.Count()}"]);

            var c = onchoSheet.Columns["V"] as Range;
            c.NumberFormat = "@";

            for (var i = 0; i < onchoEpirfData.Count(); i++)
            {
                onchoSheet.Cells[i + firstCell, "A"] = onchoEpirfData[i].TypeOfsurvey;
                onchoSheet.Cells[i + firstCell, "B"] = onchoEpirfData[i].State;
                onchoSheet.Cells[i + firstCell, "C"] = onchoEpirfData[i].NameOfadministrativeLevel2;
                onchoSheet.Cells[i + firstCell, "D"] = onchoEpirfData[i].NameOfCommunitySurveyed;
                onchoSheet.Cells[i + firstCell, "E"] = onchoEpirfData[i].Month;
                onchoSheet.Cells[i + firstCell, "F"] = onchoEpirfData[i].Year;
                onchoSheet.Cells[i + firstCell, "G"] = onchoEpirfData[i].Latitude;
                onchoSheet.Cells[i + firstCell, "H"] = onchoEpirfData[i].Longitude;
                onchoSheet.Cells[i + firstCell, "I"] = onchoEpirfData[i].Date1stPcRound;
                onchoSheet.Cells[i + firstCell, "J"] = onchoEpirfData[i].TreatmentStrategy;
                onchoSheet.Cells[i + firstCell, "K"] = onchoEpirfData[i].PrecontrolPrevalence;
                onchoSheet.Cells[i + firstCell, "L"] = onchoEpirfData[i].RoundOfPcDelivered;
                onchoSheet.Cells[i + firstCell, "M"] = onchoEpirfData[i].SkinnipDiagMethod;
                onchoSheet.Cells[i + firstCell, "N"] = onchoEpirfData[i].SkinnipExamined;
                onchoSheet.Cells[i + firstCell, "O"] = onchoEpirfData[i].SkinnipAge;
                onchoSheet.Cells[i + firstCell, "P"] = onchoEpirfData[i].SkinnipPositive;
                onchoSheet.Cells[i + firstCell, "Q"] = onchoEpirfData[i].SkinnippercentagePositive;
                onchoSheet.Cells[i + firstCell, "R"] = onchoEpirfData[i].Cmfl;
                onchoSheet.Cells[i + firstCell, "S"] = onchoEpirfData[i].SerologyDiagnostic;
                onchoSheet.Cells[i + firstCell, "T"] = onchoEpirfData[i].SerSamplingMethods;
                onchoSheet.Cells[i + firstCell, "U"] = onchoEpirfData[i].SerNumberOfPeopleExamined;
                onchoSheet.Cells[i + firstCell, "V"] = onchoEpirfData[i].SerAgeGoup;
                onchoSheet.Cells[i + firstCell, "W"] = onchoEpirfData[i].SerPositive;
                onchoSheet.Cells[i + firstCell, "X"] = onchoEpirfData[i].SerPercentagePositive;
                onchoSheet.Cells[i + firstCell, "Y"] = onchoEpirfData[i].BlackFliesExamined;
                onchoSheet.Cells[i + firstCell, "Z"] = onchoEpirfData[i].SpeciesPcr;
                onchoSheet.Cells[i + firstCell, "AA"] = onchoEpirfData[i].PercentagePoolScreenPositice;
                onchoSheet.Cells[i + firstCell, "AB"] = onchoEpirfData[i].SpeciesCrab;
                onchoSheet.Cells[i + firstCell, "AC"] = onchoEpirfData[i].CrabExamined;
                onchoSheet.Cells[i + firstCell, "AD"] = onchoEpirfData[i].PercentagEmfPositive;
            }

            onchoSheet.Protect();
        }

        private void FillOtherEpirfFile(Workbook epirfWorkBook, MetabaseCardEpirfQuery rowsData, Excel.Worksheet onchoSheet)
        {
            onchoSheet.Unprotect("MDA");
            epirfWorkBook.Unprotect("MDA");
            var newOnchoSheet = (Worksheet)epirfWorkBook.Worksheets.Add(After: epirfWorkBook.Sheets[epirfWorkBook.Sheets.Count]);
            newOnchoSheet.Name = "ONCHO Raw";

            onchoSheet.Range["A6:AE6"].Copy();
            newOnchoSheet.Range["A1:AE1"].PasteSpecial(XlPasteType.xlPasteValues);
            //onchoSheet.Range[$"A8:AE{rowsData.Data.Rows.Count()+8}"].Copy();
            //newOnchoSheet.Range[$"A2:AE{rowsData.Data.Rows.Count()}"].PasteSpecial(XlPasteType.xlPasteValues);

            for (var i = 0; i < rowsData.Data.Rows.Count(); i++)
            {
                newOnchoSheet.Range[$"A{i + 2}"].Formula = $"=ONCHO!A{i + 8}";
                newOnchoSheet.Range[$"B{i + 2}"].Formula = $"=ONCHO!B{i + 8}";
                newOnchoSheet.Range[$"C{i + 2}"].Formula = $"=ONCHO!C{i + 8}";
                newOnchoSheet.Range[$"D{i + 2}"].Formula = $"=ONCHO!D{i + 8}";
                newOnchoSheet.Range[$"E{i + 2}"].Formula = $"=ONCHO!E{i + 8}";
                newOnchoSheet.Range[$"F{i + 2}"].Formula = $"=ONCHO!F{i + 8}";
                newOnchoSheet.Range[$"G{i + 2}"].Formula = $"=ONCHO!G{i + 8}";
                newOnchoSheet.Range[$"H{i + 2}"].Formula = $"=ONCHO!H{i + 8}";
                newOnchoSheet.Range[$"I{i + 2}"].Formula = $"=ONCHO!I{i + 8}";
                newOnchoSheet.Range[$"J{i + 2}"].Formula = $"=ONCHO!J{i + 8}";
                newOnchoSheet.Range[$"K{i + 2}"].Formula = $"=ONCHO!K{i + 8}";
                newOnchoSheet.Range[$"L{i + 2}"].Formula = $"=ONCHO!L{i + 8}";
                newOnchoSheet.Range[$"M{i + 2}"].Formula = $"=ONCHO!M{i + 8}";
                newOnchoSheet.Range[$"N{i + 2}"].Formula = $"=ONCHO!N{i + 8}";
                newOnchoSheet.Range[$"O{i + 2}"].Formula = $"=ONCHO!O{i + 8}";
                newOnchoSheet.Range[$"P{i + 2}"].Formula = $"=ONCHO!P{i + 8}";
                newOnchoSheet.Range[$"Q{i + 2}"].Formula = $"=ONCHO!Q{i + 8}";
                newOnchoSheet.Range[$"R{i + 2}"].Formula = $"=ONCHO!R{i + 8}";
                newOnchoSheet.Range[$"S{i + 2}"].Formula = $"=ONCHO!S{i + 8}";
                newOnchoSheet.Range[$"T{i + 2}"].Formula = $"=ONCHO!T{i + 8}";
                newOnchoSheet.Range[$"U{i + 2}"].Formula = $"=ONCHO!U{i + 8}";
                newOnchoSheet.Range[$"V{i + 2}"].Formula = $"=ONCHO!V{i + 8}";
                newOnchoSheet.Range[$"W{i + 2}"].Formula = $"=ONCHO!W{i + 8}";
                newOnchoSheet.Range[$"X{i + 2}"].Formula = $"=ONCHO!X{i + 8}";
                newOnchoSheet.Range[$"Y{i + 2}"].Formula = $"=ONCHO!Y{i + 8}";
                newOnchoSheet.Range[$"Z{i + 2}"].Formula = $"=ONCHO!Z{i + 8}";
                newOnchoSheet.Range[$"AA{i + 2}"].Formula = $"=ONCHO!AA{i + 8}";
                newOnchoSheet.Range[$"AB{i + 2}"].Formula = $"=ONCHO!AB{i + 8}";
                newOnchoSheet.Range[$"AC{i + 2}"].Formula = $"=ONCHO!AC{i + 8}";
                newOnchoSheet.Range[$"AD{i + 2}"].Formula = $"=ONCHO!AD{i + 8}";
            }
        }
    }
}
