using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.ViewModels.Shared.EntityViewModels
{
    public class AssignmentFileViewModel : FileInfoViewModel
    {
        public IEnumerable<AssignmentSubmissionsViewModel> Assignments { get; set; }
    }
}
