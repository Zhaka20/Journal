using Journal.BLLtoUIData.DTOs;

namespace Journal.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class AssignmentSubmissionsPageData
    {
        public AssignmentSubmissionsPageData(AssignmentDTO assignment)
        {
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; set; }
    }
}