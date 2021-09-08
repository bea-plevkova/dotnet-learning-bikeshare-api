using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        //Connection to db, using Entity framework Core
        protected readonly StrvTemplateDbContext _dbContext;

        /// <summary>
        /// Injecting the context
        /// </summary>
        /// <param name="dbContext">Injection is in statup.cs</param>
        public BaseRepository(StrvTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get entity by its id, can be overriden.
        /// </summary>
        /// <param name="id">Id of the entity use for entities index with Int</param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Get List of specified entity, can be overriden.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> List()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get List of specified entity, can be overriden.
        /// </summary>
        /// <param name="predicate">Predicate - filter the list, so you do not have to fetch everything and then filter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Insert entity into database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity in database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Provide anonymous function, that should executed as transaction. Any exceptions that occur during this call are throwed.
        /// </summary>
        /// <param name="anonymousAsyncFunction">Pass an asynchronous function, that should be called as a transaction.</param>
        /// <param name="level">Specify isolation level</param>
        /// <returns></returns>
        public async Task ExecuteAsTransaction(Func<Task> anonymousAsyncFunction, IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await anonymousAsyncFunction();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
    }
}
