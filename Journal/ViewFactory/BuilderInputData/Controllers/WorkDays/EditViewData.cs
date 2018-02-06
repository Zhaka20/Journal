using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class EditViewData
    {
        private WorkDayDTO workDayDTO;

        public EditViewData(WorkDayDTO workDayDTO)
        {
            this.workDayDTO = workDayDTO;
        }
    }
}