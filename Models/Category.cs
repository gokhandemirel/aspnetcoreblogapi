using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreblogapi.Models
{
    public class Category
    {
        public Category()
        {
            PostCategory = new HashSet<PostCategory>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsActive { get; set; }
        public int IsPublish { get; set; }


        public virtual ICollection<PostCategory> PostCategory { get; set; }
    }
}
