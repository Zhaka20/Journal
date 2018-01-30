using Journal.AbstractDAL.AbstractRepositories.Common;

namespace Journal.AbstractDAL.AbstractRepositories
{
    public interface IJournalRepository : IGenericRepository<DataModel.Models.Journal, int>
    {
    }
}
