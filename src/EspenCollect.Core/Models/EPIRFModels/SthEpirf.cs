namespace EspenCollect.Core.Models
{
    public class SthEpirf
    {
        public string SurveyType { get; set; }
        public string IuName { get; set; }
        public string CommunityName { get; set; }
        public string NumberOfRoundsPC { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string AgeGroupSurveyed { get; set; }
        public string DiagnosticTest { get; set; }
        public int? AscarisNumberOfPeopleExamined { get; set; }
        public int? AscarisNumberOfPeoplePositive { get; set; }
        public float? AscarisPercentagePositive { get; set; }
        public float? AscarisPercentageHeavy { get; set; }
        public float? AscarisPercentageModerate { get; set; }
        public int? HookwormNumberOfPeopleExamined { get; set; }
        public int? HookwormNumberOfPeoplePositive { get; set; }
        public float? HookwormPercentagePositive { get; set; }
        public float? HookwormPercentageHeavy { get; set; }
        public float? HookwormPercentageModerate { get; set; }
        public int? TrichurisNumberOfPeopleExamined { get; set; }
        public int? TrichurisNumberOfPeoplePositive { get; set; }
        public float? TrichurisPercentagePositive { get; set; }
        public float? TrichurisPercentageHeavy { get; set; }
        public float? TrichurisPercentageModerate { get; set; }
        public int? SthExamined { get; set; }
        public int? SthPositive { get; set; }
        public float? SthPercentagePositve { get; set; }

    }
}
