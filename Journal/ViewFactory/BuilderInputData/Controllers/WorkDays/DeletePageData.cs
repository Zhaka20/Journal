using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class DeletePageData
    {
        private WorkDayDTO workDayDTO;

        public DeletePageData(WorkDayDTO workDayDTO)
        {
            this.workDayDTO = workDayDTO;
        }

        public WorkDayDTO WorkDay { get; set; }
    }
}