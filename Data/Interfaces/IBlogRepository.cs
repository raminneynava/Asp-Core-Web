using Data.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IBlogRepository:IRepository<BlogCategory>
    {
        Task<List<BlogCategory>> GetByIsActive();
       
    }
}