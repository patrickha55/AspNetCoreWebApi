using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        /// <summary>
        /// This one get method allow you to get information base on what type of expression you choose and if you want to include any additional data with it.
        /// </summary>
        /// <param name="expression">Optional</param>
        /// <param name="orderBy">Optional</param>
        /// <param name="includes">Optional</param>
        /// <returns>A list of objects</returns>
        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (includes is not null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            
            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// This one get method allow you to get information base on what type of expression you choose and if you want to include any additional data with it.
        /// </summary>
        /// <param name="expression">Lambda expression to filter out the information</param>
        /// <param name="includes">Additional information to include with this data (Optional)</param>
        /// <returns>A generic type object</returns>
        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes is not null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(T viewModel)
        {
            await _db.AddAsync(viewModel);
        }

        public async Task InsertRange(IEnumerable<T> viewModels)
        {
            await _db.AddRangeAsync(viewModels);
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);

            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> viewModels)
        {
            _db.RemoveRange(viewModels);
        }

        /// <summary>
        /// This method receive an object and tell ef to look up the any data in the db for changing
        /// </summary>
        /// <param name="viewModel">Object to update</param>
        public void Update(T viewModel)
        {
            _db.Attach(viewModel);
            _context.Entry(viewModel).State = EntityState.Modified;
        }
    }
}