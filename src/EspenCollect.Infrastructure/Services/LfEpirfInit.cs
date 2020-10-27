namespace EspenCollect.Services
{
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

        public async Task DispatchToEpirfSheet(string id, Worksheet epirfSheet)
        {
            var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            FillEpirfFile(epirfSheet, rowsData);
        }

        private void FillEpirfFile(Worksheet lfSheet, MetabaseCardEpirfQuery rowsData)
        {
            var lfEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirfLfModel(rowsData);

            lfSheet.Unprotect("MDA");

            lfSheet.Range["A17:AC17"].Copy(lfSheet.Range[$"A17:AC{16 + lfEpirfData.Count()}"]);

            for (var i = 0; i < lfEpirfData.Count(); i++)
            {
                lfSheet.Cells[i + 17, "A"] = lfEpirfData[i].TypeOfSurvey;
                lfSheet.Cells[i + 17, "B"] = lfEpirfData[i].EuName;
                lfSheet.Cells[i + 17, "C"] = lfEpirfData[i].IuName;
                lfSheet.Cells[i + 17, "D"] = lfEpirfData[i].SiteName;
                lfSheet.Cells[i + 17, "E"] = lfEpirfData[i].Month;
                lfSheet.Cells[i + 17, "F"] = lfEpirfData[i].Year;
                lfSheet.Cells[i + 17, "G"] = lfEpirfData[i].Latitude;
                lfSheet.Cells[i + 17, "H"] = lfEpirfData[i].Longitude;
                lfSheet.Cells[i + 17, "I"] = lfEpirfData[i].DateFirsrPcRound;
                lfSheet.Cells[i + 17, "J"] = lfEpirfData[i].NumberOfPcRoundDeliveres;
                lfSheet.Cells[i + 17, "K"] = lfEpirfData[i].DiagnosticTest;
                lfSheet.Cells[i + 17, "L"] = lfEpirfData[i].AgeGroupSurveyedMinMax;
                lfSheet.Cells[i + 17, "M"] = lfEpirfData[i].SurveySite;
                lfSheet.Cells[i + 17, "N"] = lfEpirfData[i].SurveyType;
                lfSheet.Cells[i + 17, "O"] = lfEpirfData[i].TargetSampleSize;
                lfSheet.Cells[i + 17, "P"] = lfEpirfData[i].NumberOfPeopleExamined;
                lfSheet.Cells[i + 17, "Q"] = lfEpirfData[i].NumberOfPeoplePositive;
                lfSheet.Cells[i + 17, "R"] = lfEpirfData[i].PrecentagePositive;
                lfSheet.Cells[i + 17, "S"] = lfEpirfData[i].NumberOfInvalidTests;
                lfSheet.Cells[i + 17, "T"] = lfEpirfData[i].Decision;
                lfSheet.Cells[i + 17, "U"] = lfEpirfData[i].LymphoedemaTotalNumberOfPatients;
                lfSheet.Cells[i + 17, "V"] = lfEpirfData[i].LymphoedemaMethodOfPatientEstimation;
                lfSheet.Cells[i + 17, "W"] = lfEpirfData[i].LymphoedemaDateOfPatientEstimation;
                lfSheet.Cells[i + 17, "X"] = lfEpirfData[i].LymphoedemaNbrHealthFacilities;
                lfSheet.Cells[i + 17, "Y"] = lfEpirfData[i].HydrocoeleTotalNumberOfPatients;
                lfSheet.Cells[i + 17, "Z"] = lfEpirfData[i].HydrocoeleMethodOfPatientEstimation;
                lfSheet.Cells[i + 17, "AA"] = lfEpirfData[i].HydrocoeleDateOfPatientEstimation;
                lfSheet.Cells[i + 17, "AB"] = lfEpirfData[i].HydrocoeleNumberOfHealthFacilities;
                lfSheet.Cells[i + 17, "AC"] = lfEpirfData[i].Comments;
            }

            lfSheet.Protect();
        }

    }
}
