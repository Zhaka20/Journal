using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Mentors
{
    public class MentorsListViewModel
    {
        public IEnumerable<MentorViewModel> Mentors { get; set; }
        public MentorViewModel MentorVM { get; set; }
    }
}