using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class AcceptStudentPageData
    {
        public AcceptStudentPageData(IEnumerable<StudentDTO> students)
        {
            Students = students;
        }

        public IEnumerable<StudentDTO> Students { get; internal set; }
    }

}