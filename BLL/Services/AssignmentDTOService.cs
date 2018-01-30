using Journal.AbstractBLL.AbstractServices;
using Journal.DataModel.Models;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class AssignmentDTOService : GenericDTOService<AssignmentDTO,Assignment, int>, IAssignmentDTOService
    {
        public AssignmentDTOService(IGenericService<Assignment, int> entityService, IObjectToObjectMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
