namespace EspenCollect.Core
{
    public class MetabaseCardEpirfQuery
    {
        public bool RowCount { get; set; }
        public CardDataContentType Data { get; set; }


        public class CardDataContentType
        {
            public object[][] Rows { get; set; }
        }
    }
}
