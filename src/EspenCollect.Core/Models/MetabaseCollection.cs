namespace EspenCollect.Core
{
    using System.Collections.Generic;

    public class MetabaseCollection
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<MetabaseCollection> MetabaseInnerCollections { get; set; }
    }
}
