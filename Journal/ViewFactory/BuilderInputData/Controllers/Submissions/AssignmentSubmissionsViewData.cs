using Journal.BLLtoUIData.DTOs;

namespace Journal.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class AssignmentSubmissionsViewData
    {
        public AssignmentSubmissionsViewData(AssignmentDTO assignment)
        {
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; set; }
    }
}