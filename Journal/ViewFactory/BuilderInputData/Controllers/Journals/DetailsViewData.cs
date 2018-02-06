using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal
{
    public class DetailsViewData
    {
        public DetailsViewData(JournalDTO journal)
        {
            Journal = journal;
        }

        public JournalDTO Journal { get; internal set; }
    }
}