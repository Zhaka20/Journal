using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class IndexViewData
    {
        public IndexViewData(IEnumerable<AssignmentDTO> assignments)
        {
            Assignments = assignments;
        }

        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
    }
}