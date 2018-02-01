using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class IndexPageData
    {
        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
    }
}