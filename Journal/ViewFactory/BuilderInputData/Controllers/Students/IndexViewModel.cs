using Journal.ViewModels.Shared.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journal.ViewModels.Controller.Students
{
    public class IndexViewModel
    {
        public StudentViewModel StudentModel { get; set; }
        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}