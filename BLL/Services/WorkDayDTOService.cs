using BLL.Services.Common;
using BLL.Services.Common.Abstract;
using Journal.AbstractBLL.AbstractServices;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;
using Journal.DataModel.Models;

namespace Journal.BLL.Services.Concrete
{

    public class WorkDayDTOService : GenericDTOService<WorkDayDTO, WorkDay, int>,  IWorkDayDTOService
    {
        public WorkDayDTOService(IGenericService<WorkDay, int> entityService, IObjectToObjectMapper mapper) : base(entityService, mapper)
        {
        }
    }

}
