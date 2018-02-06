using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class DeleteViewData
    {
        private WorkDayDTO workDayDTO;

        public DeleteViewData(WorkDayDTO workDayDTO)
        {
            this.workDayDTO = workDayDTO;
        }

        public WorkDayDTO WorkDay { get; set; }
    }
}