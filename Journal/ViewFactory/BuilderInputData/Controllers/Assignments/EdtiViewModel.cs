using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Journal.ViewModels.Controller.Assignments
{
    public class EdtiViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
        public int AssignmentId { get; set; }
    }
}