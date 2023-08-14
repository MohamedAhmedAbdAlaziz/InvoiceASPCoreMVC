using Core.Models.Entities;

namespace Core.Models.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetEntityWithSpec(ISpecifications<T> spec);
        Task<List<T>> ListAsync(ISpecifications<T> spec);
        Task<int> CountAsync(ISpecifications<T> spec);
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        void DeleteAll(List<T> Entities);
    }
}
