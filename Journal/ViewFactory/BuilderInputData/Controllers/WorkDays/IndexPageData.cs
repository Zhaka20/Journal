using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class IndexPageData
    {
        public IEnumerable<WorkDayDTO> WorkDays { get; set; }
    }
}