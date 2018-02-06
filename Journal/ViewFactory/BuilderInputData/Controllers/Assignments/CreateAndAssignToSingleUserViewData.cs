
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class CreateAndAssignToSingleUserViewData
    {
        private StudentDTO studentDTO;

        public CreateAndAssignToSingleUserViewData(StudentDTO studentDTO)
        {
            this.studentDTO = studentDTO;
        }

        public StudentDTO Student { get; internal set; }
    }
}