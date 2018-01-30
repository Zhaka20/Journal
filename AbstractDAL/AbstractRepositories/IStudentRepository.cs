using Journal.AbstractDAL.AbstractRepositories.Common;
using Journal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.AbstractDAL.AbstractRepositories
{
    public interface IStudentRepository : IGenericRepository<Student, string>
    {
    }
}
