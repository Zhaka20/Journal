using System;

namespace Journal.BLLtoUIData.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Edited { get; set; }

        public int AssignmentId { get; set; }
        public virtual AssignmentDTO Assignment { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUserDTO Author { get; set; }
    }
}
