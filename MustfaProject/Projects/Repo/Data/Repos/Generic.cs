using Core.Entites;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repo.Data.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Repo.Data.Repos
{
    public class Generic<T> : IGeneric<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public Generic(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;

        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity is null) { throw new NullReferenceException("No Such Entity Was Found"); }
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<T> GetAsync(string id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity is null) { throw new NullReferenceException("No Such Entity Was Found"); }
            return entity;

        }


        public async Task<IEnumerable<T>> GetAllWithSpec(ISpec<T> Spec)
        {
            try
            {
                var query = Spec_Evaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<T> GetWithSpec(ISpec<T> Spec)
        {
            var item = await Spec_Evaluator<T>.GetQuery(_dbContext.Set<T>(), Spec).FirstOrDefaultAsync();
            return item;
        }
    }
}
