namespace Journal.BLLtoUIData.DTOs
{
    public class SubmitFileDTO : FileInfoDTO
    {
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public virtual SubmissionDTO Submission { get; set; }
    }
}
