using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal
{
    public class DeleteViewData
    {
        public DeleteViewData(JournalDTO journal)
        {
            Journal = journal;
        }

        public JournalDTO Journal { get; internal set; }
    }
}