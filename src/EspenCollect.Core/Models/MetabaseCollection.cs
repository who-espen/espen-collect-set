namespace EspenCollect.Core
{
    using System.Collections.Generic;

    public class MetabaseCollection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public string Slug { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public string PersonalOwnerId { get; set; }

        public List<MetabaseCollection> MetabaseInnerCollections { get; set; }
    }
}
