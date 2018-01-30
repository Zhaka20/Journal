using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.BLLtoUIData.DTOs
{
    public class AssignmentFileDTO : FileInfoDTO
    {
        public ICollection<AssignmentDTO> Assignments { get; set; }
    }
}
