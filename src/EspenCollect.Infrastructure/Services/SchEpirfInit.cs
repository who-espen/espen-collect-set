namespace EspenCollect.Services
{
    using System.Threading.Tasks;
    using EspenCollect.Core;
    using EspenCollect.Helpers;
    using Microsoft.Office.Interop.Excel;

    public class SchEpirfInit : ISchEpirfInit
    {
        private readonly IRestApi _restApi;

        public SchEpirfInit(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public async Task DispatchToEpirfSheet(string id, Worksheet epirfSheet)
        {
            var rowsData = await _restApi.GetEpirfCard(id).ConfigureAwait(false);

            FillEpirfFile(epirfSheet, rowsData);
        }

        private void FillEpirfFile(Worksheet sthSheet, MetabaseCardEpirfQuery rowsData)
        {
            var sthEpirfData = MetabaseCardToEpirfModel.MetabaseCardToEpirSchfModel(rowsData);

            sthSheet.Unprotect("MDA");

            sthSheet.Range["A8:S8"].Copy(sthSheet.Range[$"A8:S{7 + sthEpirfData.Count}"]);

            for (var i = 0; i < sthEpirfData.Count; i++)
            {
                sthSheet.Cells[i + 8, "A"] = sthEpirfData[i].SurveyType;
                sthSheet.Cells[i + 8, "B"] = sthEpirfData[i].IuName;
                sthSheet.Cells[i + 8, "C"] = sthEpirfData[i].SiteName;
                sthSheet.Cells[i + 8, "D"] = sthEpirfData[i].Month;
                sthSheet.Cells[i + 8, "E"] = sthEpirfData[i].Year;
                sthSheet.Cells[i + 8, "F"] = sthEpirfData[i].Latitude;
                sthSheet.Cells[i + 8, "G"] = sthEpirfData[i].Longitude;
                sthSheet.Cells[i + 8, "H"] = sthEpirfData[i].AgeGroup;
                sthSheet.Cells[i + 8, "I"] = sthEpirfData[i].DiagnosticTest;
                sthSheet.Cells[i + 8, "J"] = sthEpirfData[i].UrinaryNumberOfPeopleExamined;
                sthSheet.Cells[i + 8, "K"] = sthEpirfData[i].UrinaryNumberofPositive;
                sthSheet.Cells[i + 8, "L"] = sthEpirfData[i].UrinaryPercentageOfPositive;
                sthSheet.Cells[i + 8, "M"] = sthEpirfData[i].UrinaryPercentageHeavy;
                sthSheet.Cells[i + 8, "N"] = sthEpirfData[i].UrinaryPercentageLow;
                sthSheet.Cells[i + 8, "O"] = sthEpirfData[i].IntestinalNumberOfPeopleExamined;
                sthSheet.Cells[i + 8, "P"] = sthEpirfData[i].IntestinalNumberofPositive;
                sthSheet.Cells[i + 8, "Q"] = sthEpirfData[i].IntestinalPercentageOfPositive;
                sthSheet.Cells[i + 8, "R"] = sthEpirfData[i].IntestinalPercentageHeavy;
                sthSheet.Cells[i + 8, "S"] = sthEpirfData[i].IntestinalPercentageModerate;
            }

            sthSheet.Protect();
        }
    }
}
