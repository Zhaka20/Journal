using Journal.ViewModels.Shared.EntityViewModels;
using System.ComponentModel.DataAnnotations;

namespace Journal.ViewModels.Controller.Attendances
{
    public class DeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public AttendanceViewModel Attendance { get; set; }
    }
}