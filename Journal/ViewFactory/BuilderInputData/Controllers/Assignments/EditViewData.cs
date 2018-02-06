using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class EdtiViewData
    {
        public EdtiViewData(AssignmentDTO assignment)
        {
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; internal set; }
    }
}