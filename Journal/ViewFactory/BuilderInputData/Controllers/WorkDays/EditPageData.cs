using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class EditPageData
    {
        private WorkDayDTO workDayDTO;

        public EditPageData(WorkDayDTO workDayDTO)
        {
            this.workDayDTO = workDayDTO;
        }
    }
}