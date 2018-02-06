using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class HomeViewData
    {
        public HomeViewData(MentorDTO mentor)
        {
            Mentor = mentor;
        }

        public MentorDTO Mentor { get; internal set; }
    }
}