using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Linq;



namespace Academy.Core.Repositories
{
    public class SkillLevelsRepository : BaseRepository, ISkillLevelsRepository
    {
        public void Add(SkillLevel skillLevel)
        {
            db.SkillLevels.Add(skillLevel);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.SkillLevels.Remove(db.SkillLevels.Single(c => c.Id == id));
            db.SaveChanges();
        }

        public IQueryable<SkillLevel> Get()
        {
           return db.SkillLevels;
        }

        public IQueryable<SkillLevel> Get(int id)
        {
            return db.SkillLevels.Where(m => m.Id == id);
        }
        public void Update(SkillLevel tag)
        {
            db.SkillLevels.Update(tag);
            db.SaveChanges();
        }
    }
}
