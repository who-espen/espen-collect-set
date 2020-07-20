namespace EspenCollect.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core.Models;
    using EspenCollect.Helpers;
    using Excel = Microsoft.Office.Interop.Excel;

    public class LfEpirfGenerator : ILfEpirfGenerator
    {
        private readonly IRestApi _restApi;

        public LfEpirfGenerator(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public async Task GenerateLfEpirfAsync(string id, string path)
        {
            //var lfRowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            //var filePath = Path.GetFullPath(@"Resources\WHO_EPIRF_PC.xlsm");
            //var excelApp = new Excel.Application
            //{
            //    Visible = false
            //};

            //var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            //var lfSheet = epirfWorkBook.Worksheets.get_Item("LF") as Excel.Worksheet;

            //var lfEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirfOnchoModel(lfRowsData);

            //FillEpirfFile(lfSheet, lfEpirfData.ToList());

            //epirfWorkBook.SaveAs(path);
            //epirfWorkBook.Close(true);
            //excelApp.Visible = true;
            //excelApp.Quit();
        }

        private void FillEpirfFile(Excel.Worksheet lfSheet, IList<LfEpirf> onchoEpirfData)
        {
            lfSheet.Unprotect("MDA");

            lfSheet.Range["A17:AC17"].Copy(lfSheet.Range[$"A17:AC{16 + onchoEpirfData.Count()}"]);

            for (var i = 0; i < onchoEpirfData.Count(); i++)
            {
                lfSheet.Cells[i + 17, "A"] = onchoEpirfData[i].TypeOfSurvey;
                lfSheet.Cells[i + 17, "B"] = onchoEpirfData[i].EuName;
                lfSheet.Cells[i + 17, "C"] = onchoEpirfData[i].IuName;
                lfSheet.Cells[i + 17, "D"] = onchoEpirfData[i].SiteName;
                lfSheet.Cells[i + 17, "E"] = onchoEpirfData[i].Month;
                lfSheet.Cells[i + 17, "F"] = onchoEpirfData[i].Year;
                lfSheet.Cells[i + 17, "G"] = onchoEpirfData[i].Latitude;
                lfSheet.Cells[i + 17, "H"] = onchoEpirfData[i].Longitude;
                lfSheet.Cells[i + 17, "I"] = onchoEpirfData[i].DateFirsrPcRound;
                lfSheet.Cells[i + 17, "J"] = onchoEpirfData[i].NumberOfPcRoundDeliveres;
                lfSheet.Cells[i + 17, "K"] = onchoEpirfData[i].DiagnosticTest;
                lfSheet.Cells[i + 17, "L"] = onchoEpirfData[i].AgeGroupSurveyedMinMax;
                lfSheet.Cells[i + 17, "M"] = onchoEpirfData[i].SurveySite;
                lfSheet.Cells[i + 17, "N"] = onchoEpirfData[i].SurveyType;
                lfSheet.Cells[i + 17, "O"] = onchoEpirfData[i].TargetSampleSize;
                lfSheet.Cells[i + 17, "P"] = onchoEpirfData[i].NumberOfPeopleExamined;
                lfSheet.Cells[i + 17, "Q"] = onchoEpirfData[i].NumberOfPeoplePositive;
                lfSheet.Cells[i + 17, "R"] = onchoEpirfData[i].PrecentagePositive;
                lfSheet.Cells[i + 17, "S"] = onchoEpirfData[i].NumberOfInvalidTests;
                lfSheet.Cells[i + 17, "T"] = onchoEpirfData[i].Decision;
                lfSheet.Cells[i + 17, "U"] = onchoEpirfData[i].LymphoedemaTotalNumberOfPatients;
                lfSheet.Cells[i + 17, "V"] = onchoEpirfData[i].LymphoedemaMethodOfPatientEstimation;
                lfSheet.Cells[i + 17, "W"] = onchoEpirfData[i].LymphoedemaDateOfPatientEstimation;
                lfSheet.Cells[i + 17, "X"] = onchoEpirfData[i].LymphoedemaNbrHealthFacilities;
                lfSheet.Cells[i + 17, "Y"] = onchoEpirfData[i].HydrocoeleTotalNumberOfPatients;
                lfSheet.Cells[i + 17, "Z"] = onchoEpirfData[i].HydrocoeleMethodOfPatientEstimation;
                lfSheet.Cells[i + 17, "AA"] = onchoEpirfData[i].HydrocoeleDateOfPatientEstimation;
                lfSheet.Cells[i + 17, "AB"] = onchoEpirfData[i].HydrocoeleNumberOfHealthFacilities;
                lfSheet.Cells[i + 17, "AC"] = onchoEpirfData[i].Comments;
            }

            lfSheet.Protect();
        }

    }
}
