
using Core.Models.Entities;
using Core.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucrture.Infrastrucrture
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }

        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }



        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }
        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
        public void DeleteAll(List<T> Entities)
        {
            _context.Set<T>().RemoveRange(Entities);
        }
        public void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            _context.Entry(Entity).State = EntityState.Modified;

        }
        public async Task<T> GetEntityWithSpec(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();

        }

        public async Task<List<T>> ListAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }



        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}