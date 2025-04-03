using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Interfaces
{
    public interface IGeneric<T> where T : BaseEntity
    {
      
        Task<T> AddAsync(T entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> GetAllWithSpec(ISpec<T> Spec);
        Task<T> GetWithSpec(ISpec<T> Spec);







    }
}
