using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Manage
{
    public class ConfigureTwoFactorViewData
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}