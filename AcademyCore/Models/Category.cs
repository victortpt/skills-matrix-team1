
using System.Collections.Generic;

namespace Academy.Core.Models
{
    public partial class Category
    {
        public Category()
        {
            Skills = new HashSet<Skill>();
        }

        public long Id { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
