using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class DetailsViewData
    {
        public DetailsViewData(AssignmentDTO assignment)
        {
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; internal set; }
    }
}