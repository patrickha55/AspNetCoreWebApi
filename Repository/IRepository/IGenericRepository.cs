using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// This get method allow you to get information base on what type of expression you choose and if you want to include any additional data with it.
        /// </summary>
        /// <param name="expression">Optional</param>
        /// <param name="orderBy">Optional</param>
        /// <param name="includes">Optional</param>
        /// <returns>A list of objects</returns>
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );
        /// <summary>
        /// This get method allow you to get information base on what type of expression you choose and if you want to include any additional data with it.
        /// </summary>
        /// <param name="expression">Lambda expression to filter out the information</param>
        /// <param name="includes">Additional information to include with this data (Optional)</param>
        /// <returns>A generic type object</returns>
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Insert(T viewModel);
        Task InsertRange(IEnumerable<T> viewModels);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> viewModels);
        /// <summary>
        /// This method receive an object and tell ef to look up the any data in the db for changing
        /// </summary>
        /// <param name="viewModel">Object to update</param>
        void Update(T viewModel);
    }
}