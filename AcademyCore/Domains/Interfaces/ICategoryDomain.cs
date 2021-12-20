using Academy.Core.Models;


namespace Academy.Core.Domains.Interfaces
{
    public interface ICategoryDomain
    {
        public string GetCategorySkills();
        public Category Get(int id);
        public void Update(Category category);
        public void Add(Category category);
        public void Delete(int id);
        public  bool CheckCagetoryExists(Category category);
        public bool CheckIfAssociatedSkills(int categoryId);
        public string RemoveAccentsWithNormalization(string inputString);
    }
}