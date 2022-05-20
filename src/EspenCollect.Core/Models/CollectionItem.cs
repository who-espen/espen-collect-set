namespace EspenCollect.Core
{
    using System.Collections.Generic;

    public class CollectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string CollectionPosition { get; set; }
        public string Display  { get; set; }
        public string Favorite { get; set; }
        public string Model { get; set; }
    }

    public class CollectionItemParent
    {
        public List<CollectionItem> Data { get; set; }
    }
}
