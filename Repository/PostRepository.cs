using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcoreblogapi.Models;

namespace aspnetcoreblogapi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }


        public async Task<IList<Post>> GetAll()
        {
            return await _context.Post.Include(x=> x.PostCategory).Include(y=> y.PostTag).Where(z=> z.IsActive == 1).ToListAsync();
        }
    }
}
