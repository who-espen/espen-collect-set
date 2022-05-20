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

                        TypeOfsurvey = rows[i][0]?.ToString(),
                        State = rows[i][1]?.ToString(),
                        NameOfadministrativeLevel2 = rows[i][2]?.ToString(),
                        NameOfCommunitySurveyed = rows[i][3]?.ToString(),
                        Month = rows[i][4]?.ToString(),
                        Year = int.Parse(rows[i][5].ToString()),
                        Latitude = rows[i][6].GetValueOrNull<decimal>(),
                        Longitude = rows[i][7].GetValueOrNull<decimal>(),
                        Date1stPcRound = rows[i][8]?.ToString(),
                        TreatmentStrategy = rows[i][9]?.ToString(),
                        PrecontrolPrevalence = rows[i][10]?.ToString(),
                        RoundOfPcDelivered = rows[i][11].GetValueOrNull<int>(),
                        SkinnipDiagMethod = rows[i][12]?.ToString(),
                        SkinnipExamined = rows[i][13]?.ToString(),
                        SkinnipAge = rows[i][14]?.ToString(),
                        SkinnipPositive = rows[i][15].GetValueOrNull<int>(),
                        SkinnippercentagePositive = rows[i][16].GetValueOrNull<decimal>(),
                        Cmfl = rows[i][17]?.ToString(),
                        SerologyDiagnostic = rows[i][18]?.ToString(),
                        SerSamplingMethods = rows[i][19]?.ToString(),
                        SerNumberOfPeopleExamined = rows[i][20].GetValueOrNull<int>(),
                        SerAgeGoup = rows[i][21]?.ToString(),
                        SerPositive = rows[i][22].GetValueOrNull<int>(),
                        SerPercentagePositive = rows[i][23].GetValueOrNull<decimal>(),
                        BlackFliesExamined = rows[i][24].GetValueOrNull<int>(),
                        SpeciesPcr = rows[i][25]?.ToString(),
                        PercentagePoolScreenPositice = rows[i][26].GetValueOrNull<decimal>(),
                        SpeciesCrab = rows[i][27]?.ToString(),
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
                        TypeOfSurvey = rows[i][0]?.ToString(),
                        EuName = rows[i][1]?.ToString(),
                        IuName = rows[i][2]?.ToString(),
                        SiteName = rows[i][3]?.ToString(),
                        Month = rows[i][4]?.ToString(),
                        Year = rows[i][5].GetValueOrNull<int>(),
                        Latitude = rows[i][6].GetValueOrNull<decimal>(),
                        Longitude = rows[i][7].GetValueOrNull<decimal>(),
                        DateFirsrPcRound = rows[i][8]?.ToString(),
                        NumberOfPcRoundDeliveres = rows[i][9].GetValueOrNull<int>(),
                        DiagnosticTest = rows[i][10]?.ToString(),
                        AgeGroupSurveyedMinMax = rows[i][11]?.ToString(),
                        SurveySite = rows[i][12]?.ToString(),
                        SurveyType = rows[i][13]?.ToString(),
                        TargetSampleSize = rows[i][14]?.ToString(),
                        NumberOfPeopleExamined = rows[i][15].GetValueOrNull<int>(),
                        NumberOfPeoplePositive = rows[i][16].GetValueOrNull<int>(),
                        PrecentagePositive = rows[i][17].GetValueOrNull<decimal>(),
                        NumberOfInvalidTests = rows[i][18].GetValueOrNull<int>(),
                        Decision = rows[i][19]?.ToString(),
                        LymphoedemaTotalNumberOfPatients = rows[i][20].GetValueOrNull<int>(),
                        LymphoedemaMethodOfPatientEstimation = rows[i][21].GetValueOrNull<int>(),
                        LymphoedemaDateOfPatientEstimation = rows[i][22]?.ToString(),
                        LymphoedemaNbrHealthFacilities = rows[i][23].GetValueOrNull<int>(),
                        HydrocoeleTotalNumberOfPatients = rows[i][24].GetValueOrNull<int>(),
                        HydrocoeleMethodOfPatientEstimation = rows[i][25].GetValueOrNull<int>(),
                        HydrocoeleDateOfPatientEstimation = rows[i][26]?.ToString(),
                        HydrocoeleNumberOfHealthFacilities = rows[i][27].GetValueOrNull<int>(),
                        Comments = rows[i][28]?.ToString(),

                    }); ;
                }
            }

            return lfEpirfs;
        }


        internal static IList<SthEpirf> MetabaseCardToEpirSthfModel(MetabaseCardEpirfQuery metabaseCardEpirfQuery)
        {
            var rows = metabaseCardEpirfQuery.Data.Rows;
            var sthEpirfs = new List<SthEpirf>();

            if (rows.Any())
            {
                for (var i = 0; i < rows.Count(); i++)
                {
                    sthEpirfs.Add(new SthEpirf
                    {
                        SurveyType = rows[i][0]?.ToString(),
                        IuName = rows[i][1]?.ToString(),
                        CommunityName = rows[i][2]?.ToString(),
                        NumberOfRoundsPC = rows[i][3].GetValueOrNull<int>(),
                        Month = rows[i][4]?.ToString(),
                        Year = rows[i][5].GetValueOrNull<int>(),
                        Latitude = rows[i][6].GetValueOrNull<decimal>(),
                        Longitude = rows[i][7].GetValueOrNull<decimal>(),
                        AgeGroupSurveyed = rows[i][8]?.ToString(),
                        DiagnosticTest = rows[i][9]?.ToString(),
                        AscarisNumberOfPeopleExamined = rows[i][10].GetValueOrNull<int>(),
                        AscarisNumberOfPeoplePositive = rows[i][11].GetValueOrNull<int>(),
                        AscarisPercentagePositive = rows[i][12].GetValueOrNull<float>(),
                        AscarisPercentageHeavy = rows[i][13].GetValueOrNull<float>(),
                        AscarisPercentageModerate = rows[i][14].GetValueOrNull<float>(),
                        HookwormNumberOfPeopleExamined = rows[i][15].GetValueOrNull<int>(),
                        HookwormNumberOfPeoplePositive = rows[i][16].GetValueOrNull<int>(),
                        HookwormPercentagePositive = rows[i][17].GetValueOrNull<float>(),
                        HookwormPercentageHeavy = rows[i][18].GetValueOrNull<float>(),
                        HookwormPercentageModerate = rows[i][19].GetValueOrNull<float>(),
                        TrichurisNumberOfPeopleExamined = rows[i][20].GetValueOrNull<int>(),
                        TrichurisNumberOfPeoplePositive = rows[i][21].GetValueOrNull<int>(),
                        TrichurisPercentagePositive = rows[i][22].GetValueOrNull<float>(),
                        TrichurisPercentageHeavy = rows[i][23].GetValueOrNull<float>(),
                        TrichurisPercentageModerate = rows[i][24].GetValueOrNull<float>(),
                        SthExamined = rows[i][25].GetValueOrNull<int>(),
                        SthPositive = rows[i][26].GetValueOrNull<int>(),
                        SthPercentagePositve = rows[i][27].GetValueOrNull<float>()

                    }); ;
                }
            }

            return sthEpirfs;
        }


        internal static IList<SchEpirf> MetabaseCardToEpirSchfModel(MetabaseCardEpirfQuery metabaseCardEpirfQuery)
        {
            var rows = metabaseCardEpirfQuery.Data.Rows;
            var schEpirfs = new List<SchEpirf>();

            if (rows.Any())
            {
                for (var i = 0; i < rows.Count(); i++)
                {
                    schEpirfs.Add(new SchEpirf
                    {
                        SurveyType = rows[i][0]?.ToString(),
                        IuName = rows[i][1]?.ToString(),
                        SiteName = rows[i][2]?.ToString(),
                        Month = rows[i][3]?.ToString(),
                        Year = rows[i][4].GetValueOrNull<int>(),
                        Latitude = rows[i][5].GetValueOrNull<decimal>(),
                        Longitude = rows[i][6].GetValueOrNull<decimal>(),
                        AgeGroup = rows[i][7]?.ToString(),
                        DiagnosticTest = rows[i][8]?.ToString(),
                        UrinaryNumberOfPeopleExamined = rows[i][9]?.ToString(),
                        UrinaryNumberofPositive = rows[i][10]?.ToString(),
                        UrinaryPercentageOfPositive = rows[i][11].GetValueOrNull<float>(),
                        UrinaryPercentageHeavy = rows[i][12].GetValueOrNull<float>(),
                        UrinaryPercentageLow = rows[i][13].GetValueOrNull<float>(),
                        IntestinalNumberOfPeopleExamined = rows[i][14]?.ToString(),
                        IntestinalNumberofPositive = rows[i][15]?.ToString(),
                        IntestinalPercentageOfPositive = rows[i][16].GetValueOrNull<float>(),
                        IntestinalPercentageHeavy = rows[i][17].GetValueOrNull<float>(),
                        IntestinalPercentageModerate = rows[i][18].GetValueOrNull<float>()
                    });
                }
            }

            return schEpirfs;
        }

    }
}
