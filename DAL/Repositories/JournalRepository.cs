using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.DataModel.Models;
using Journal.DAL.Context;
using System;

namespace Journal.DAL.Repositories
{
    public class JournalRepository : GenericRepository<DataModel.Models.Journal, int>, IJournalRepository,IDisposable
    {
        public JournalRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
