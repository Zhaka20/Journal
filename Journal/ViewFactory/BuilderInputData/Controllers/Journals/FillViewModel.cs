using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.Journals
{
    public class FillViewModel
    {
        public JournalViewModel Journal { get; set; }
        public WorkDayViewModel WorkDayModel { get; set; }
    }
}