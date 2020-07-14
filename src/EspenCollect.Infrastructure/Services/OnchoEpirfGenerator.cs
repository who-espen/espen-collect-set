namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using EspenCollect.Data.Models;
    using EspenCollect.Helpers;
    using Excel = Microsoft.Office.Interop.Excel;

    public class OnchoEpirfGenerator : IOnchoEpirfGenerator
    {
        private readonly IRestApi _restApi;

        public OnchoEpirfGenerator(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public async Task GenerateOnchoEpirfAsync(string id)
        {
            var onchoRowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            var onchoEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirfOnchoModel(onchoRowsData);

            FillEpirfFile(onchoSheet, onchoEpirfData.ToList());

            //epirfWorkBook.Save(@"C:\Users\Dyesse\Desktop");
            epirfWorkBook.SaveAs(@"C:\Users\Dyesse\Desktop\ToDeleteEpirf.xlsm");
            epirfWorkBook.Close(true);
            excelApp.Visible = true;
            excelApp.Quit();
        }

        private void FillEpirfFile(Excel.Worksheet onchoSheet, IList<OnchoEpirf> onchoEpirfData) {

            onchoSheet.Unprotect("MDA");

            onchoSheet.Range["A8:AE8"].Copy(onchoSheet.Range[$"A8:AE{7 + onchoEpirfData.Count()}"]);

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
