using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.DataModel.Models;
using System;
using Journal.DAL.Context;

namespace Journal.DAL.Repositories
{
    public class SubmitFileRepository : GenericRepository<SubmitFile, int>, ISubmitFileRepository, IDisposable
    {
        public SubmitFileRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}