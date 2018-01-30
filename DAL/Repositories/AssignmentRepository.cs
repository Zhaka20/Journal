using Journal.DAL.Repositories.Common;
using System;
using Journal.DAL.Context;
using Journal.DataModel.Models;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.DAL.Repositories
{
    public class AssignmentRepository : GenericRepository<Assignment, int>, IAssignmentRepository, IDisposable
    {
        public AssignmentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
