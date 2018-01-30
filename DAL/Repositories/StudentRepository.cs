using System;
using Journal.DAL.Context;
using Journal.DataModel.Models;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.DAL.Repositories.Common;

namespace Journal.DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student, string>, IStudentRepository, IDisposable
    {
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
