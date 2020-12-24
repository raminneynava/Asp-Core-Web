using Data.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BlogRepository : Repository<BlogCategory>, IBlogRepository
    {
        public BlogRepository(AppDbContext dbContext)
        : base(dbContext)
        {

        }
        public async Task<List<BlogCategory>> GetByIsActive()
        {
            try
            {
                return await TableNoTracking.Where(x => x.IsActive).Include(x => x.Blogs).ToListAsync();

            }
            catch (Exception ex)
            {
            
                throw;
            }
        }
    }
}
