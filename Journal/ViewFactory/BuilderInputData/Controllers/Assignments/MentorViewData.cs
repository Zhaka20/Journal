using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class MentorViewData
    {
        public MentorViewData(IEnumerable<AssignmentDTO> assignments, MentorDTO mentor)
        {
            Assignments = assignments;
            Mentor = mentor;
        }

        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
        public MentorDTO Mentor { get; internal set; }
    }
}