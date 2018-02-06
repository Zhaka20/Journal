using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class IndexViewData
    {
        private IEnumerable<WorkDayDTO> workDayDTOs;

        public IndexViewData(IEnumerable<WorkDayDTO> workDayDTOs)
        {
            this.workDayDTOs = workDayDTOs;
        }
    }
}