namespace EspenCollect.Core
{
    using System.Collections.Generic;

    public class EpirfCardsIds
    {
        public EpirfCardsIds()
        {
            OnchoIds = new List<string>();

            LfIds = new List<string>();

            SchIds = new List<string>();

            SthIds = new List<string>();
        }

        public List<string> OnchoIds { get; set; }
        public List<string> LfIds { get; set; }
        public List<string> SchIds { get; set; }
        public List<string> SthIds { get; set; }
    }
}
