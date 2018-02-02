using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal
{
    public class FillPageData
    {
        public FillPageData(JournalDTO journal)
        {
            Journal = journal;
        }

        public JournalDTO Journal { get; internal set; }
    }
}