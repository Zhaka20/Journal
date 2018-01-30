using System.Collections.Generic;

namespace Journal.BLLtoUIData.DTOs
{
    public class StudentDTO : ApplicationUserDTO
    {
        public string  MentorId { get; set; }
        public virtual MentorDTO Mentor { get; set; }

        public virtual ICollection<AttendanceDTO> Attendances { get; set; }
        public virtual ICollection<SubmissionDTO> Submissions { get; set; }
    }
}
