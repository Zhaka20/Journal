using Journal.AbstractBLL.AbstractServices;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.BLL.Services.Concrete
{
    public class JournalDTOService : GenericDTOService<JournalDTO, DataModel.Models.Journal, int>, IJournalDTOService
    {
        public JournalDTOService(IJournalRepository repository, IObjectToObjectMapper mapper) : base(repository, mapper)
        {
        }
    }
}
