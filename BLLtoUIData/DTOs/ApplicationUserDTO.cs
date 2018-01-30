using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Journal.BLLtoUIData.DTOs
{
    public class ApplicationUserDTO : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + "  " + LastName; }
        }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}
