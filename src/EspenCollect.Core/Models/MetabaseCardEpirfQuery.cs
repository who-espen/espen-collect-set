namespace EspenCollect.Core
{
    using System.Collections.Generic;

    public class MetabaseCardEpirfQuery
    {
        public bool RowCount { get; set; }
        public CardDataContentType Data { get; set; }


        public class CardDataContentType
        {
            public List<object[]> Rows { get; set; }
        }
    }
}
