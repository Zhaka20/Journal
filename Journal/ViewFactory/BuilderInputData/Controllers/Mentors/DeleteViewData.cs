using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class DeleteViewData
    {
        public DeleteViewData(MentorDTO mentor)
        {
            Mentor = mentor;
        }

        public MentorDTO Mentor { get; internal set; }
    }

}