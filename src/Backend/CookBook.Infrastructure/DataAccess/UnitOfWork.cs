using CookBook.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CookBookDbContext _dbContext;

        public UnitOfWork(CookBookDbContext context)
        {
            _dbContext = context;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
