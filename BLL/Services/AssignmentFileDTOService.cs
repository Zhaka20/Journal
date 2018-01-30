using Journal.AbstractBLL.AbstractServices;
using Journal.DataModel.Models;
using Journal.AbstractDAL.AbstractRepositories;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class AssignmentFileDTOService : GenericDTOService<AssignmentFileDTO, AssignmentFile, int>, IAssignmentFileDTOService
    {
        public AssignmentFileDTOService(IGenericService<AssignmentFile, int> entityService, IObjectToObjectMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
