using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Database.Repositories.Base
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> List();

        Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task ExecuteAsTransaction(Func<Task> anonymousAsyncFunction, IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
