using Journal.AbstractBLL.AbstractServices;
using Journal.DataModel.Models;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common;
using BLL.Services.Common.Abstract;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.BLL.Services.Concrete
{
    public class AssignmentDTOService : GenericDTOService<AssignmentDTO,Assignment, int>, IAssignmentDTOService
    {
        public AssignmentDTOService(IAssignmentRepository repository, IObjectToObjectMapper mapper) : base(repository, mapper)
        {
        }
    }
}
