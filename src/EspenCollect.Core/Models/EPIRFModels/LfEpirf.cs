namespace EspenCollect.Core.Models
{
    public class LfEpirf
    {
        public string TypeOfSurvey { get; set; }
        public string EuName { get; set; }
        public string IuName { get; set; }
        public string SiteName { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string DateFirsrPcRound { get; set; }
        public int? NumberOfPcRoundDeliveres { get; set; }
        public string DiagnosticTest { get; set; }
        public string AgeGroupSurveyedMinMax { get; set; }
        public string SurveySite { get; set; }
        public string SurveyType { get; set; }
        public string TargetSampleSize { get; set; }
        public int? NumberOfPeopleExamined { get; set; }
        public int? NumberOfPeoplePositive { get; set; }
        public decimal? PrecentagePositive { get; set; }
        public int? NumberOfInvalidTests { get; set; }
        public string Decision { get; set; }
        public int? LymphoedemaTotalNumberOfPatients { get; set; }
        public int? LymphoedemaMethodOfPatientEstimation { get; set; }
        public string LymphoedemaDateOfPatientEstimation { get; set; }
        public int? LymphoedemaNbrHealthFacilities { get; set; }
        public int? HydrocoeleTotalNumberOfPatients { get; set; }
        public int? HydrocoeleMethodOfPatientEstimation { get; set; }
        public string HydrocoeleDateOfPatientEstimation { get; set; }
        public int? HydrocoeleNumberOfHealthFacilities { get; set; }
        public string Comments { get; set; }
    }
}
