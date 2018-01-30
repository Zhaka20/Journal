using System;

namespace Journal.BLLtoUIData.DTOs
{
    public class StudentAttendanceDTO
    {
        public int Id { get; set; }
        public StudentDTO Student { get; set; }
        public DateTime? Came { get; set; }
        public DateTime? Left { get; set; }
    }
}
