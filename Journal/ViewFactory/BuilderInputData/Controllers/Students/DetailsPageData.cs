using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class DetailsPageData
    {
        public DetailsPageData(StudentDTO student)
        {
            Student = student;
        }

        public StudentDTO Student { get; set; }
    }
}