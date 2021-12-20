using System;


namespace Academy.Core.Repositories
{
    public class BaseRepository : IDisposable
    {
        protected DatabaseContext db;

        public BaseRepository()
        {
            db = new DatabaseContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
