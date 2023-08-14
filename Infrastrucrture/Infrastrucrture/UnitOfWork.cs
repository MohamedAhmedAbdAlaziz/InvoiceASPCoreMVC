
using Core.Models.Entities;
using Core.Models.Interfaces;
using System.Collections;

namespace Infrastrucrture.Infrastrucrture
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var RepositoryType = typeof(GenericRepository<>);
                var RepositoryInstance = Activator.CreateInstance(RepositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, RepositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[type];

        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}