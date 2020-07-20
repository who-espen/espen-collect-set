namespace EspenCollect.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using EspenCollect.Core;
    using EspenCollect.Core.Models;

    public class MetabaseCardToEpirfModel
    {
        internal static IList<OnchoEpirf> MetabaseCardToEpirfOnchoModel(MetabaseCardEpirfQuery metabaseCardEpirfQuery)
        {
            var rows = metabaseCardEpirfQuery.Data.Rows;
            var onchoEpirfs = new List<OnchoEpirf>();

            if (rows.Any())
            {
                for (var i = 0; i < rows.Count(); i++)
                {
                    onchoEpirfs.Add(new OnchoEpirf
                    {

                        TypeOfsurvey = rows[i][0] as string,
                        State = rows[i][1] as string,
                        NameOfadministrativeLevel2 = rows[i][2] as string,
                        NameOfCommunitySurveyed = rows[i][3] as string,
                        Month = rows[i][4] as string,
                        Year = int.Parse(rows[i][5].ToString()),
                        Latitude = rows[i][6].GetValueOrNull<decimal>(),
                        Longitude = rows[i][7].GetValueOrNull<decimal>(),
                        Date1stPcRound = rows[i][8] as string,
                        TreatmentStrategy = rows[i][9] as string,
                        PrecontrolPrevalence = rows[i][10] as string,
                        RoundOfPcDelivered = rows[i][11].GetValueOrNull<int>(),
                        SkinnipDiagMethod = rows[i][12] as string,
                        SkinnipExamined = rows[i][13] as string,
                        SkinnipAge = rows[i][14] as string,
                        SkinnipPositive = rows[i][15].GetValueOrNull<int>(),
                        SkinnippercentagePositive = rows[i][16].GetValueOrNull<decimal>(),
                        Cmfl = rows[i][17] as string,
                        SerologyDiagnostic = rows[i][18] as string,
                        SerSamplingMethods = rows[i][19] as string,
                        SerNumberOfPeopleExamined = rows[i][20].GetValueOrNull<int>(),
                        SerAgeGoup = rows[i][21] as string,
                        SerPositive = rows[i][22].GetValueOrNull<int>(),
                        SerPercentagePositive = rows[i][23].GetValueOrNull<decimal>(),
                        BlackFliesExamined = rows[i][24].GetValueOrNull<int>(),
                        SpeciesPcr = rows[i][25] as string,
                        PercentagePoolScreenPositice = rows[i][26].GetValueOrNull<decimal>(),
                        SpeciesCrab = rows[i][27] as string,
                        CrabExamined = rows[i][28].GetValueOrNull<int>(),
                        PercentagEmfPositive = rows[i][29].GetValueOrNull<decimal>(),

                    }); ;
                }
            }

            return onchoEpirfs;
        }

        internal static IList<LfEpirf> MetabaseCardToEpirfLfModel(MetabaseCardEpirfQuery metabaseCardEpirfQuery)
        {
            var rows = metabaseCardEpirfQuery.Data.Rows;
            var lfEpirfs = new List<LfEpirf>();

            if (rows.Any())
            {
                for (var i = 0; i < rows.Count(); i++)
                {
                    lfEpirfs.Add(new LfEpirf
                    {
                        TypeOfSurvey = rows[i][0] as string,
                        EuName = rows[i][1] as string,
                        IuName = rows[i][2] as string,
                        SiteName = rows[i][3] as string,
                        Month = rows[i][4] as string,
                        Year = rows[i][5].GetValueOrNull<int>(),
                        Latitude = rows[i][6].GetValueOrNull<decimal>(),
                        Longitude = rows[i][7].GetValueOrNull<decimal>(),
                        DateFirsrPcRound = rows[i][8] as string,
                        NumberOfPcRoundDeliveres = rows[i][9].GetValueOrNull<int>(),
                        DiagnosticTest = rows[i][10] as string,
                        AgeGroupSurveyedMinMax = rows[i][11] as string,
                        SurveySite = rows[i][12] as string,
                        SurveyType = rows[i][13] as string,
                        TargetSampleSize = rows[i][14] as string,
                        NumberOfPeopleExamined = rows[i][15].GetValueOrNull<int>(),
                        NumberOfPeoplePositive = rows[i][16].GetValueOrNull<int>(),
                        PrecentagePositive = rows[i][17].GetValueOrNull<decimal>(),
                        NumberOfInvalidTests = rows[i][18].GetValueOrNull<int>(),
                        Decision = rows[i][19] as string,
                        LymphoedemaTotalNumberOfPatients = rows[i][20].GetValueOrNull<int>(),
                        LymphoedemaMethodOfPatientEstimation = rows[i][21].GetValueOrNull<int>(),
                        LymphoedemaDateOfPatientEstimation = rows[i][22] as string,
                        LymphoedemaNbrHealthFacilities = rows[i][23].GetValueOrNull<int>(),
                        HydrocoeleTotalNumberOfPatients = rows[i][24].GetValueOrNull<int>(),
                        HydrocoeleMethodOfPatientEstimation = rows[i][25].GetValueOrNull<int>(),
                        HydrocoeleDateOfPatientEstimation = rows[i][26] as string,
                        HydrocoeleNumberOfHealthFacilities = rows[i][27].GetValueOrNull<int>(),
                        Comments = rows[i][28] as string,

                    }); ;
                }
            }

            return lfEpirfs;
        }
    }
}
