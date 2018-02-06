using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors
{
    public class DetailsViewData
    {
        public DetailsViewData(MentorDTO mentor)
        {
            Mentor = mentor;
        }

        public MentorDTO Mentor { get; internal set; }
    }
}