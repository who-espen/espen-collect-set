
namespace EspenCollect.Data.Models
{
    public class OnchoEpirf
    {
        public string TypeOfsurvey { get; set; }
        public string State { get; set; }
        public string NameOfadministrativeLevel2 { get; set; }
        public string NameOfCommunitySurveyed { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Date1stPcRound { get; set; }
        public string TreatmentStrategy { get; set; }
        public string PrecontrolPrevalence { get; set; }
        public int RoundOfPcDelivered { get; set; }
        public string SkinnipDiagMethod { get; set; }
        public string SkinnipExamined { get; set; }
        public string SkinnipAge { get; set; }
        public int SkinnipPositive { get; set; }
        public float SkinnippercentagePositive { get; set; }
        public string Cmfl { get; set; }
        public string SerologyDiagnostic { get; set; }
        public string SerSamplingMethods { get; set; }
        public int SerNumberOfPeopleExamined { get; set; }
        public string SerAgeGoup { get; set; }
        public int SerPositive { get; set; }
        public float SerPercentagePositive { get; set; }
        public int BlackFliesExamined { get; set; }
        public string SpeciesPcr { get; set; }
        public float PercentagePoolScreenPositice { get; set; }
        public string SpeciesCrab { get; set; }
        public float PercentagEmfPositive { get; set; }

    }
}
