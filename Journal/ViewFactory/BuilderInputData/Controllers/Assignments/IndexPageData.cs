using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class IndexPageData
    {
        public IndexPageData(IEnumerable<AssignmentDTO> assignments)
        {
            Assignments = assignments;
        }

        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
    }
}