using Journal.ViewModels.Controller.Attendances;
using System;
using System.Threading.Tasks;

namespace Journal.Services.Abstractions
{
    public interface IAttendancesControllerService : IDisposable
    {
        Task<IndexViewModel> GetAttendancesIndexViewModelAsync();
        Task<DetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId);
        Task<EditViewModel> GetEditAttendanceViewModelAsync(int attendanceId);
        Task UpdateAsync(EditViewModel inputModel);
        Task<DeleteViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId);
        Task DeleteAsync(DeleteInputModel inputModel);
    }
}
