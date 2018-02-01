using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class MentorPageData
    {
        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
        public MentorDTO Mentor { get; internal set; }
    }
}