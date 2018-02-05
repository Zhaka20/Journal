using Journal.AbstractDAL.AbstractRepositories.Common;
using Journal.DataModel.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Journal.AbstractDAL.AbstractRepositories
{
    public interface IAssignmentFileRepository : IGenericRepository<AssignmentFile,int>
    {
    }
}
