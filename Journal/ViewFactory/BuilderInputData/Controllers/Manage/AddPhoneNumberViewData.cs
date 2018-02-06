using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Manage
{
    public class AddPhoneNumberViewData
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}