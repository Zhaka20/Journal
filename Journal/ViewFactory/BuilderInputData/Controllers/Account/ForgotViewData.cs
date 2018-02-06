using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Account
{
    public class ForgotViewData
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}