using System;
using System.Collections.Generic;
using Academy.Core.Models;
using Academy.Core.DTO;


namespace Academy.Core.Domains.Interfaces
{
    public interface IMemberSkillsDomain
    {
        public List<MemberSkill> Get();
        public void Update(MemberSkill members);
        public void Add(MemberSkill members);
        public void Delete(long id);

    }
}