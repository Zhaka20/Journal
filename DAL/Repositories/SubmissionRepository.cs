using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.DataModel.Models;
using Journal.DAL.Context;
using System;

namespace Journal.DAL.Repositories
{
    public class SubmissionRepository : GenericRepository<Submission, object[]>, ISubmissionRepository, IDisposable
    {
        public SubmissionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
