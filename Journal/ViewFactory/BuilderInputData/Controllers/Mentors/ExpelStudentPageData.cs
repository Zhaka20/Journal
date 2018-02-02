using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class ExpelStudentPageData
    {
        public ExpelStudentPageData(StudentDTO student)
        {
            Student = student;
        }

        public StudentDTO Student { get; internal set; }
    }
}