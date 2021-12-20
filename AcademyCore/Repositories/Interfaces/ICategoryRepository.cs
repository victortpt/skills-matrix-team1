using Academy.Core.Models;
using System.Linq;

namespace Academy.Core.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public IQueryable<Category> GetCategory();

        public IQueryable<Category> Get(int id);

        public void Add(Category category);

        public void Update(Category category);

        public void Delete(int id);
    }
}
