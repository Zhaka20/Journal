using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal
{
    public class IndexPageData
    {
        public IndexPageData(IEnumerable<JournalDTO> journals)
        {
            Journals = journals;
        }

        public IEnumerable<JournalDTO> Journals { get; internal set; }
    }
}