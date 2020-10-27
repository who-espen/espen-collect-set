namespace EspenCollect.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Microsoft.Office.Interop.Excel;

    public class SthEpirfInit : ISthEpirfInit
    {
        private readonly IRestApi _restApi;

        public SthEpirfInit(IRestApi restApi)
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
            var lfEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirSthfModel(rowsData);

            lfSheet.Unprotect("MDA");

            lfSheet.Range["A8:AB8"].Copy(lfSheet.Range[$"A8:AB{7 + lfEpirfData.Count()}"]);

            for (var i = 0; i < lfEpirfData.Count(); i++)
            {
                lfSheet.Cells[i + 8, "A"] = lfEpirfData[i].SurveyType;
                lfSheet.Cells[i + 8, "B"] = lfEpirfData[i].IuName;
                lfSheet.Cells[i + 8, "C"] = lfEpirfData[i].CommunityName;
                lfSheet.Cells[i + 8, "D"] = lfEpirfData[i].NumberOfRoundsPC;
                lfSheet.Cells[i + 8, "E"] = lfEpirfData[i].Month;
                lfSheet.Cells[i + 8, "F"] = lfEpirfData[i].Year;
                lfSheet.Cells[i + 8, "G"] = lfEpirfData[i].Latitude;
                lfSheet.Cells[i + 8, "H"] = lfEpirfData[i].Longitude;
                lfSheet.Cells[i + 8, "I"] = lfEpirfData[i].AgeGroupSurveyed;
                lfSheet.Cells[i + 8, "J"] = lfEpirfData[i].DiagnosticTest;
                lfSheet.Cells[i + 8, "K"] = lfEpirfData[i].AscarisNumberOfPeopleExamined;
                lfSheet.Cells[i + 8, "L"] = lfEpirfData[i].AscarisNumberOfPeoplePositive;
                lfSheet.Cells[i + 8, "M"] = lfEpirfData[i].AscarisPercentagePositive;
                lfSheet.Cells[i + 8, "N"] = lfEpirfData[i].AscarisPercentageHeavy;
                lfSheet.Cells[i + 8, "O"] = lfEpirfData[i].AscarisPercentageModerate;
                lfSheet.Cells[i + 8, "P"] = lfEpirfData[i].HookwormNumberOfPeopleExamined;
                lfSheet.Cells[i + 8, "Q"] = lfEpirfData[i].HookwormNumberOfPeoplePositive;
                lfSheet.Cells[i + 8, "R"] = lfEpirfData[i].HookwormPercentagePositive;
                lfSheet.Cells[i + 8, "S"] = lfEpirfData[i].HookwormPercentageHeavy;
                lfSheet.Cells[i + 8, "T"] = lfEpirfData[i].HookwormPercentageModerate;
                lfSheet.Cells[i + 8, "U"] = lfEpirfData[i].TrichurisNumberOfPeopleExamined;
                lfSheet.Cells[i + 8, "V"] = lfEpirfData[i].TrichurisNumberOfPeoplePositive;
                lfSheet.Cells[i + 8, "W"] = lfEpirfData[i].TrichurisPercentagePositive;
                lfSheet.Cells[i + 8, "X"] = lfEpirfData[i].TrichurisPercentageHeavy;
                lfSheet.Cells[i + 8, "Y"] = lfEpirfData[i].TrichurisPercentageModerate;
                lfSheet.Cells[i + 8, "Z"] = lfEpirfData[i].SthExamined;
                lfSheet.Cells[i + 8, "AA"] = lfEpirfData[i].SthPositive;
                lfSheet.Cells[i + 8, "AB"] = lfEpirfData[i].SthPercentagePositve;
            }

            lfSheet.Protect();
        }
    }
}
