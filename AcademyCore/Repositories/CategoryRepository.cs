using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Linq;

namespace Academy.Core.Repositories
{
    public class CategoryRepository : BaseRepository , ICategoryRepository
    {
       
        /// <summary>
        /// Gets data from Categories.
        /// Retrieved data: id and category.
        /// </summary>
        /// <returns>An IQueryable with the data from Categories table</returns>
        public IQueryable<Category> GetCategory() {
            return db.Categories;
        }

        /// <summary>
        /// Get an specific Category from the Categories table.
        /// <summary>
        /// param name="id">/param>
        /// returns>An IQueryable with the data from a Category within the Categories table/returns>
        public IQueryable<Category> Get(int id) {
            return db.Categories.Where(m => m.Id == id);
        }

    
        /// <summary>
        /// Adds a category into the Categories table.
        /// <summary>
        /// <param name="category">/param>
        public void Add(Category newCategory) {
            db.Categories.Add(newCategory);
            db.SaveChanges();
        }

        /// summary>
        /// Updates the data of a Category.
        /// /summary>
        /// param name="id">/param>
        public void Update(Category category) {
            Category updateCategoryDB = db.Categories.Where(d => d.Id == category.Id).First();
            updateCategoryDB.Category1 = category.Category1;
            db.SaveChanges();
        }

        /// summary>
        /// Deletes a specific Category from Categories.
        /// /summary>
        /// param name="id">/param>
        public void Delete(int id) {
            db.Categories.Remove(db.Categories.Single(c => c.Id == id));
            db.SaveChanges();
        }

    }
}