using Journal.DAL.Repositories.Common;
using Journal.DataModel.Models;
using Journal.DAL.Context;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment, int>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
