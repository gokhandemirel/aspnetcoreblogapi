using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcoreblogapi.Models;

namespace aspnetcoreblogapi.Repository
{
    public interface IPostRepository
    {
        Task<IList<Post>> GetAll();
    }
}
