using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.WorkDays
{
    public class IndexViewModel
    {
        public IEnumerable<WorkDayViewModel> WorkDays { get; set; }
        public WorkDayViewModel WorkDay { get; set; }
    }
}