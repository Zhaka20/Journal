using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal
{
    public class IndexPageData
    {
        public IEnumerable<JournalDTO> Journals { get; internal set; }
    }
}