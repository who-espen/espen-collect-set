namespace EspenCollect.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using EspenCollect.Core;
    using EspenCollect.Data.Models;

    public class MetabaseCardToEpirfModel
    {
        internal static IEnumerable<OnchoEpirf> MetabaseCardToEpirfOnchoModel(MetabaseCardEpirfQuery metabaseCardEpirfQuery)
        {
            var rows = metabaseCardEpirfQuery.Data.Rows;
            var onchoEpirfs = new List<OnchoEpirf>();

            if (rows.Any())
            {
                for (var i = 0; i < rows.Count(); i++) {
                    onchoEpirfs.Add(new OnchoEpirf {

                        TypeOfsurvey = rows[i][0] as string,
                        State = rows[i][1] as string,
                        NameOfadministrativeLevel2 = rows[i][2] as string,
                        NameOfCommunitySurveyed = rows[i][3] as string,
                        Month = rows[i][4] as string,
                        Year = rows[i][5] as int?,
                        Latitude = rows[i][6] as decimal?,
                        Longitude = rows[i][7] as decimal?,
                        Date1stPcRound = rows[i][8] as string,
                        TreatmentStrategy = rows[i][9] as string,
                        PrecontrolPrevalence = rows[i][10] as string,
                        RoundOfPcDelivered = rows[i][11] as int?,
                        SkinnipDiagMethod = rows[i][12] as string,
                        SkinnipExamined = rows[i][13] as string,
                        SkinnipAge = rows[i][14] as string,
                        SkinnipPositive = rows[i][15] as int?,
                        SkinnippercentagePositive = rows[i][16] as decimal?,
                        Cmfl = rows[i][17] as string,
                        SerologyDiagnostic = rows[i][18] as string,
                        SerSamplingMethods = rows[i][19] as string,
                        SerAgeGoup = rows[i][20] as string,
                        SerNumberOfPeopleExamined = rows[i][21] as int?,
                        SerPositive = rows[i][22] as int?,
                        SerPercentagePositive = rows[i][23] as int?,
                        BlackFliesExamined = rows[i][24] as int?,
                        SpeciesPcr = rows[i][25] as string,
                        PercentagePoolScreenPositice = rows[i][26] as decimal?,
                        SpeciesCrab = rows[i][27] as string,
                        CrabExamined = rows[i][28] as int?,
                        PercentagEmfPositive = rows[i][29] as decimal?

                    });;
                }
            }

            return onchoEpirfs;
        }

    }
}
