using System.Collections.Generic;

namespace Journal.BLLtoUIData.DTOs
{
    public class JournalDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public ICollection<WorkDayDTO> WorkDays{ get; set; }

        public string MentorId { get; set; }
        public MentorDTO Mentor { get; set; }
    }
}
