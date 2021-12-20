using Academy.Core.Models;
using Academy.Core.DTO;
using Academy.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace Academy.Core.Repositories
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {
        public void Add(Skill skill)
        {
            db.Skills.Add(skill);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Skills.Remove(db.Skills.Single(c => c.Id == id));
            db.SaveChanges();
        }

        public IQueryable<Skill> Get()
        {
           return db.Skills;
        }

        public void Update(Skill tag)
        {
            db.Skills.Update(tag);
            db.SaveChanges();
        }
    }
}