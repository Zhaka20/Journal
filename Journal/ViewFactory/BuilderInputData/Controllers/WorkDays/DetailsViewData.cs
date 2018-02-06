using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class DetailsViewData
    {
        private WorkDayDTO workDayDTO;

        public DetailsViewData(WorkDayDTO workDayDTO)
        {
            this.workDayDTO = workDayDTO;
        }

    }
}