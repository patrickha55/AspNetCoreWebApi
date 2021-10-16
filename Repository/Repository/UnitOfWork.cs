using System;
using System.Threading.Tasks;
using HotelListing.Data;
using HotelListing.Data.Entities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);
        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}