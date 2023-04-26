using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>>GetAll();
        public Task<T> GetById(Guid id);
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<bool> Delete(Guid id);
        //TODO Получше подумать как это реализовать
        public Task<IEnumerable<T>> GetFiltered(Func<IQueryable<T>, IQueryable<T>> filter);
    }
}
