using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class StudentsAndSubmissionsListViewData
    {
        public StudentsAndSubmissionsListViewData(AssignmentDTO assignment)
        {
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; internal set; }
    }

}