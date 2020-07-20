
namespace EspenCollect.Core.Models
{
    public class OnchoEpirf
    {
        public string TypeOfsurvey { get; set; }
        public string State { get; set; }
        public string NameOfadministrativeLevel2 { get; set; }
        public string NameOfCommunitySurveyed { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Date1stPcRound { get; set; }
        public string TreatmentStrategy { get; set; }
        public string PrecontrolPrevalence { get; set; }
        public int? RoundOfPcDelivered { get; set; }
        public string SkinnipDiagMethod { get; set; }
        public string SkinnipExamined { get; set; }
        public string SkinnipAge { get; set; }
        public int? SkinnipPositive { get; set; }
        public decimal? SkinnippercentagePositive { get; set; }
        public string Cmfl { get; set; }
        public string SerologyDiagnostic { get; set; }
        public string SerSamplingMethods { get; set; }
        public int? SerNumberOfPeopleExamined { get; set; }
        public string SerAgeGoup { get; set; }
        public int? SerPositive { get; set; }
        public decimal? SerPercentagePositive { get; set; }
        public int? BlackFliesExamined { get; set; }
        public string SpeciesPcr { get; set; }
        public decimal? PercentagePoolScreenPositice { get; set; }
        public string SpeciesCrab { get; set; }
        public int? CrabExamined { get; set; }
        public decimal? PercentagEmfPositive { get; set; }
    }
}
