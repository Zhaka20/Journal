using Journal.DataModel.Models;
using Journal.AbstractBLL.AbstractServices;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class JournalDTOService : GenericDTOService<JournalDTO, DataModel.Models.Journal, int>, IJournalDTOService
    {
        public JournalDTOService(IGenericService<DataModel.Models.Journal, int> entityService, IObjectToObjectMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
