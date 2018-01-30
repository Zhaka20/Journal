using Journal.AbstractBLL.AbstractServices;
using Journal.DataModel.Models;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class SubmitFileDTOService : GenericDTOService<SubmitFileDTO, SubmitFile, int>, ISubmitFileDTOService
    {
        public SubmitFileDTOService(IGenericService<SubmitFile, int> entityService, IObjectToObjectMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
