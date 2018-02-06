using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class DetailsViewData
    {
        public DetailsViewData(StudentDTO student)
        {
            Student = student;
        }

        public StudentDTO Student { get; set; }
    }
}