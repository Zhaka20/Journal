using System;
using Journal.DAL.Context;
using Journal.DataModel.Models;
using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.DAL.Repositories
{
    public class MentorRepository : GenericRepository<Mentor, string>, IMentorRepository, IDisposable
    {
        public MentorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
