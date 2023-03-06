using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreblogapi.Models
{
    public class Post
    {
        public Post()
        {
            PostTag = new HashSet<PostTag>();
            PostCategory = new HashSet<PostCategory>();
        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsActive { get; set; }
        public int IsPublish { get; set; }


        public ICollection<PostTag> PostTag { get; set; }
        public ICollection<PostCategory> PostCategory { get; set; }
    }
}
