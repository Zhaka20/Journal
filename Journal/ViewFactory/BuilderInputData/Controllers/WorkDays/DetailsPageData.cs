using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class DetailsPageData
    {
        private WorkDayDTO workDayDTO;

        public DetailsPageData(WorkDayDTO workDayDTO)
        {
            this.workDayDTO = workDayDTO;
        }

    }
}