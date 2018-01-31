using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class AcceptStudentPageData
    {
        public IEnumerable<StudentDTO> Students { get; internal set; }
    }

}