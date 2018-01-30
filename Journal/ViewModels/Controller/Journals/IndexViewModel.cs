using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Journals
{
    public class IndexViewModel
    {
        public IEnumerable<JournalViewModel> Journals { get; set; }
        public MentorViewModel MentorModel { get; set; }
        public JournalViewModel JournalModel { get; set; }
    }
}