using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;
using System;
using Journal.DAL.Context;
using Journal.DataModel.Models;

namespace Journal.DAL.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance, int>, IAttendanceRepository, IDisposable
    {
        public AttendanceRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
