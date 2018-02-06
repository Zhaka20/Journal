using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class AcceptStudentViewData
    {
        public AcceptStudentViewData(IEnumerable<StudentDTO> students)
        {
            Students = students;
        }

        public IEnumerable<StudentDTO> Students { get; internal set; }
    }

}