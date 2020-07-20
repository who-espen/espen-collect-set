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

        public Task GenerateLfEpirfAsync(string id, string path)
        {
            throw new System.NotImplementedException();
        }

        private void FillEpirfFile(Excel.Worksheet onchoSheet, IList<LfEpirf> onchoEpirfData)
        {

            onchoSheet.Unprotect("MDA");

            onchoSheet.Range["A17:AC17"].Copy(onchoSheet.Range[$"A17:AE{16 + onchoEpirfData.Count()}"]);

            for (var i = 0; i < onchoEpirfData.Count(); i++)
            {
                onchoSheet.Cells[i + 17, "A"] = onchoEpirfData[i].TypeOfSurvey;
                onchoSheet.Cells[i + 17, "B"] = onchoEpirfData[i].EuName;
                onchoSheet.Cells[i + 17, "C"] = onchoEpirfData[i].IuName;
                onchoSheet.Cells[i + 17, "D"] = onchoEpirfData[i].SiteName;
                onchoSheet.Cells[i + 17, "E"] = onchoEpirfData[i].Month;
                onchoSheet.Cells[i + 17, "F"] = onchoEpirfData[i].Year;
                onchoSheet.Cells[i + 17, "G"] = onchoEpirfData[i].Latitude;
                onchoSheet.Cells[i + 17, "H"] = onchoEpirfData[i].Longitude;
                onchoSheet.Cells[i + 17, "I"] = onchoEpirfData[i].DateFirsrPcRound;
                onchoSheet.Cells[i + 17, "J"] = onchoEpirfData[i].NumberOfPcRoundDeliveres;
                onchoSheet.Cells[i + 17, "K"] = onchoEpirfData[i].DiagnosticTest;
                onchoSheet.Cells[i + 17, "L"] = onchoEpirfData[i].AgeGroupSurveyedMinMax;
                onchoSheet.Cells[i + 17, "M"] = onchoEpirfData[i].SurveySite;
                onchoSheet.Cells[i + 17, "N"] = onchoEpirfData[i].SurveyType;
                onchoSheet.Cells[i + 17, "O"] = onchoEpirfData[i].TargetSampleSize;
                onchoSheet.Cells[i + 17, "P"] = onchoEpirfData[i].NumberOfPeopleExamined;
                onchoSheet.Cells[i + 17, "Q"] = onchoEpirfData[i].NumberOfPeoplePositive;
                onchoSheet.Cells[i + 17, "R"] = onchoEpirfData[i].PrecentagePositive;
                onchoSheet.Cells[i + 17, "S"] = onchoEpirfData[i].NumberOfInvalidTests;
                onchoSheet.Cells[i + 17, "T"] = onchoEpirfData[i].Decision;
                onchoSheet.Cells[i + 17, "U"] = onchoEpirfData[i].LymphoedemaTotalNumberOfPatients;
                onchoSheet.Cells[i + 17, "V"] = onchoEpirfData[i].LymphoedemaMethodOfPatientEstimation;
                onchoSheet.Cells[i + 17, "W"] = onchoEpirfData[i].LymphoedemaDateOfPatientEstimation;
                onchoSheet.Cells[i + 17, "X"] = onchoEpirfData[i].LymphoedemaNbrHealthFacilities;
                onchoSheet.Cells[i + 17, "Y"] = onchoEpirfData[i].HydrocoeleTotalNumberOfPatients;
                onchoSheet.Cells[i + 17, "Z"] = onchoEpirfData[i].HydrocoeleMethodOfPatientEstimation;
                onchoSheet.Cells[i + 17, "AA"] = onchoEpirfData[i].HydrocoeleDateOfPatientEstimation;
                onchoSheet.Cells[i + 17, "AB"] = onchoEpirfData[i].HydrocoeleNumberOfHealthFacilities;
                onchoSheet.Cells[i + 17, "AC"] = onchoEpirfData[i].Comments;
            }

            onchoSheet.Protect();
        }

    }
}
