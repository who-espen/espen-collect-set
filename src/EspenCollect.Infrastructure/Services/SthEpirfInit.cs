namespace EspenCollect.Services
{
    using System.Collections.Generic;
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

        public Task DispatchToEpirfSheet2(List<string> id, Worksheet epirfSheet)
        {
            throw new System.NotImplementedException();
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
    }
}
