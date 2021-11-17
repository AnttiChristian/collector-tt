using System.Collections.Generic;

namespace Collector.Data
{
    public class CategoryRecord : EntityRecord
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<CategoryRecord> ChildCategories { get; set; } = new();
    }
}
