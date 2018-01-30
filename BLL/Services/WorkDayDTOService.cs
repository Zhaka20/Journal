using BLL.Services.Common;
using BLL.Services.Common.Abstract;
using Journal.AbstractBLL.AbstractServices;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.BLLtoUIData.DTOs;
using Journal.DataModel.Models;

namespace Journal.BLL.Services.Concrete
{

    public class WorkDayDTOService : GenericDTOService<WorkDayDTO, WorkDay, int>,  IWorkDayDTOService
    {
        public WorkDayDTOService(IWorkDayRepository repository,
                                 IObjectToObjectMapper mapper) 
                               : base(repository, mapper)
        {
        }
    }

}
