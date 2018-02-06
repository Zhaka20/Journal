using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class EditViewData
    {
        public EditViewData(MentorDTO mentor)
        {
            Mentor = mentor;
        }

        public MentorDTO Mentor { get; internal set; }
    }
}