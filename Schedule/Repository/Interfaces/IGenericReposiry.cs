using System.Collections.Generic;

namespace Schedule.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
        int Add(TEntity model);
        void Delete(TEntity model);
        void Update(TEntity model);
    }
}
