using Academy.Core.Models;
using System.Linq;

namespace Academy.Core.Repositories.Interfaces
{
    public interface IMembersRepository
    {
        public IQueryable<Member> Get();

        public IQueryable<Member> Get(long id);

        public void Add(Member member);

        public void Update(Member member);

        public void Delete(long id);
    }
}
