namespace EspenCollect.Core.Models
{
    public class SchEpirf
    {
        public string SurveyType { get; set; }
        public string IuName { get; set; }
        public string SiteName { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string AgeGroup { get; set; }
        public string DiagnosticTest { get; set; }
        public string UrinaryNumberOfPeopleExamined { get; set; }
        public string UrinaryNumberofPositive { get; set; }
        public float? UrinaryPercentageOfPositive { get; set; }
        public float? UrinaryPercentageHeavy { get; set; }
        public float? UrinaryPercentageLow { get; set; }
        public string IntestinalNumberOfPeopleExamined { get; set; }
        public string IntestinalNumberofPositive { get; set; }
        public float? IntestinalPercentageOfPositive { get; set; }
        public float? IntestinalPercentageHeavy { get; set; }
        public float? IntestinalPercentageModerate { get; set; }
    }
}
