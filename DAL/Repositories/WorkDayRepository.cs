using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;
using System;
using Journal.DAL.Context;
using Journal.DataModel.Models;

namespace Journal.DAL.Repositories
{
    public class WorkDayRepository : GenericRepository<WorkDay, int>, IWorkDayRepository, IDisposable
    {
        public WorkDayRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
