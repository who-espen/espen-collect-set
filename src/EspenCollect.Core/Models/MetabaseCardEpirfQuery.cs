namespace EspenCollect.Core
{
    using System.Collections.Generic;

    public class MetabaseCardEpirfQuery
    {
        public MetabaseCardEpirfQuery()
        {
            Data = new CardDataContentType();
        }
        public bool RowCount { get; set; }
        public CardDataContentType Data { get; set; }


        public class CardDataContentType
        {
            public CardDataContentType()
            {
                Rows = new List<object[]>();
            }

            public List<object[]> Rows { get; set; }
        }
    }
}
