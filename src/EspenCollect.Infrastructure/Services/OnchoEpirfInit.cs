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

            FillEpirfFile(onchoSheet, metabaseCard);

            return Task.CompletedTask;
        }

        private void FillEpirfFile(Worksheet onchoSheet, MetabaseCardEpirfQuery rowsData)
        {
            var onchoEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirfOnchoModel(rowsData);

            onchoSheet.Unprotect("MDA");

            onchoSheet.Range["A8:AE8"].Copy(onchoSheet.Range[$"A8:AE{7 + onchoEpirfData.Count()}"]);

            var c = onchoSheet.Columns["V"] as Range;
            c.NumberFormat = "@";

            for (var i = 0; i < onchoEpirfData.Count(); i++)
            {
                onchoSheet.Cells[i + 8, "A"] = onchoEpirfData[i].TypeOfsurvey;
                onchoSheet.Cells[i + 8, "B"] = onchoEpirfData[i].State;
                onchoSheet.Cells[i + 8, "C"] = onchoEpirfData[i].NameOfadministrativeLevel2;
                onchoSheet.Cells[i + 8, "D"] = onchoEpirfData[i].NameOfCommunitySurveyed;
                onchoSheet.Cells[i + 8, "E"] = onchoEpirfData[i].Month;
                onchoSheet.Cells[i + 8, "F"] = onchoEpirfData[i].Year;
                onchoSheet.Cells[i + 8, "G"] = onchoEpirfData[i].Latitude;
                onchoSheet.Cells[i + 8, "H"] = onchoEpirfData[i].Longitude;
                onchoSheet.Cells[i + 8, "I"] = onchoEpirfData[i].Date1stPcRound;
                onchoSheet.Cells[i + 8, "J"] = onchoEpirfData[i].TreatmentStrategy;
                onchoSheet.Cells[i + 8, "K"] = onchoEpirfData[i].PrecontrolPrevalence;
                onchoSheet.Cells[i + 8, "L"] = onchoEpirfData[i].RoundOfPcDelivered;
                onchoSheet.Cells[i + 8, "M"] = onchoEpirfData[i].SkinnipDiagMethod;
                onchoSheet.Cells[i + 8, "N"] = onchoEpirfData[i].SkinnipExamined;
                onchoSheet.Cells[i + 8, "O"] = onchoEpirfData[i].SkinnipAge;
                onchoSheet.Cells[i + 8, "P"] = onchoEpirfData[i].SkinnipPositive;
                onchoSheet.Cells[i + 8, "Q"] = onchoEpirfData[i].SkinnippercentagePositive;
                onchoSheet.Cells[i + 8, "R"] = onchoEpirfData[i].Cmfl;
                onchoSheet.Cells[i + 8, "S"] = onchoEpirfData[i].SerologyDiagnostic;
                onchoSheet.Cells[i + 8, "T"] = onchoEpirfData[i].SerSamplingMethods;
                onchoSheet.Cells[i + 8, "U"] = onchoEpirfData[i].SerNumberOfPeopleExamined;
                onchoSheet.Cells[i + 8, "V"] = onchoEpirfData[i].SerAgeGoup;
                onchoSheet.Cells[i + 8, "W"] = onchoEpirfData[i].SerPositive;
                onchoSheet.Cells[i + 8, "X"] = onchoEpirfData[i].SerPercentagePositive;
                onchoSheet.Cells[i + 8, "Y"] = onchoEpirfData[i].BlackFliesExamined;
                onchoSheet.Cells[i + 8, "Z"] = onchoEpirfData[i].SpeciesPcr;
                onchoSheet.Cells[i + 8, "AA"] = onchoEpirfData[i].PercentagePoolScreenPositice;
                onchoSheet.Cells[i + 8, "AB"] = onchoEpirfData[i].SpeciesCrab;
                onchoSheet.Cells[i + 8, "AC"] = onchoEpirfData[i].CrabExamined;
                onchoSheet.Cells[i + 8, "AD"] = onchoEpirfData[i].PercentagEmfPositive;
            }

            onchoSheet.Protect();
        }
    }
}
