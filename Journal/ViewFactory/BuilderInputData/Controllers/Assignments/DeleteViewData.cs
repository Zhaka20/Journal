
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class DeleteViewData
    {
        public DeleteViewData(AssignmentDTO assignment)
        {
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; internal set; }
    }
}