using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class MentorsListPageData
    {
        public MentorsListPageData(IEnumerable<MentorDTO> mentors)
        {
            Mentors = mentors;
        }

        public IEnumerable<MentorDTO> Mentors { get; internal set; }
    }
}