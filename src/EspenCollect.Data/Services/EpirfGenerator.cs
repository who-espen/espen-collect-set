namespace EspenCollect.Data.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using EspenCollect.Data.Repositories;
    using SpreadsheetLight;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGenerator : IEpirfGenerator
    {
        private readonly IOnchoEpirfRepository _onchoEpirfRepository;

        public EpirfGenerator(IOnchoEpirfRepository onchoEpirf)
        {
            Argument.IsNotNull(() => onchoEpirf);

            _onchoEpirfRepository = onchoEpirf;
        }

        public async  Task GenerateEpirfAsync(string filePath)
        {
            Argument.IsNotNullOrEmpty(() => filePath);

            var onchoData = await _onchoEpirfRepository.GetAllEpirfOnchoAsync().ConfigureAwait(false);

            var excelApp = new Excel.Application();

            excelApp.Visible = false;

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            prepareEirfFRow(onchoSheet, onchoData.Count);

            for (var i = 0; i < onchoData.Count; i++)
            {
                onchoSheet.Cells[i + 8, "A"] = onchoData[i].TypeOfsurvey;
                onchoSheet.Cells[i + 8, "B"] = onchoData[i].State;
                onchoSheet.Cells[i + 8, "C"] = onchoData[i].NameOfadministrativeLevel2;
                onchoSheet.Cells[i + 8, "D"] = onchoData[i].NameOfCommunitySurveyed;
                onchoSheet.Cells[i + 8, "E"] = onchoData[i].Month;
                onchoSheet.Cells[i + 8, "F"] = onchoData[i].Year;
                onchoSheet.Cells[i + 8, "G"] = onchoData[i].Latitude;
                onchoSheet.Cells[i + 8, "H"] = onchoData[i].Longitude;
                onchoSheet.Cells[i + 8, "I"] = onchoData[i].Date1stPcRound;
                onchoSheet.Cells[i + 8, "J"] = onchoData[i].TreatmentStrategy;
                onchoSheet.Cells[i + 8, "K"] = onchoData[i].PrecontrolPrevalence;
                onchoSheet.Cells[i + 8, "L"] = onchoData[i].RoundOfPcDelivered;
                onchoSheet.Cells[i + 8, "M"] = onchoData[i].SkinnipDiagMethod;
                onchoSheet.Cells[i + 8, "N"] = onchoData[i].SkinnipExamined;
                onchoSheet.Cells[i + 8, "O"] = onchoData[i].SkinnipAge;
                onchoSheet.Cells[i + 8, "P"] = onchoData[i].SkinnipPositive;
                onchoSheet.Cells[i + 8, "R"] = onchoData[i].Cmfl;
                onchoSheet.Cells[i + 8, "S"] = onchoData[i].SerologyDiagnostic;
                onchoSheet.Cells[i + 8, "T"] = onchoData[i].SerSamplingMethods;
                onchoSheet.Cells[i + 8, "U"] = onchoData[i].SerNumberOfPeopleExamined;
                onchoSheet.Cells[i + 8, "V"] = onchoData[i].SerAgeGoup;
                onchoSheet.Cells[i + 8, "W"] = onchoData[i].SerPositive;
                onchoSheet.Cells[i + 8, "Y"] = onchoData[i].BlackFliesExamined;
                onchoSheet.Cells[i + 8, "Z"] = onchoData[i].SpeciesPcr;
                onchoSheet.Cells[i + 8, "AA"] = onchoData[i].PercentagePoolScreenPositice;
                onchoSheet.Cells[i + 8, "AB"] = onchoData[i].SpeciesCrab;
                onchoSheet.Cells[i + 8, "AC"] = onchoData[i].CrabExamined;
                onchoSheet.Cells[i + 8, "AD"] = onchoData[i].PercentagEmfPositive;

            }

            epirfWorkBook.Save();
            epirfWorkBook.Close(true);
            excelApp.Quit();
        }

        private void prepareEirfFRow(Excel.Worksheet onchoSheet, int lenghth)
        {
            onchoSheet.Unprotect();

            onchoSheet.Range["A8:AE8"].Copy(onchoSheet.Range[$"A8:AE{7 + lenghth}"]);

            onchoSheet.Protect();
        }
    }
}
