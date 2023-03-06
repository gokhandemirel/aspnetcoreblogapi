using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreblogapi.Models
{
    public class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsActive { get; set; }
        public int IsPublish { get; set; }


        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
