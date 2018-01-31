using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Journal.ViewModels.Controller.Attendances;
using Journal.BLLtoUIData.DTOs;
using Journal.AbstractBLL.AbstractServices;

namespace Journal.Services.ControllerServices
{
    public class AttendancesControllerService : IAttendancesControllerService
    {
        protected readonly IAttendanceDTOService service;
        public AttendancesControllerService(IAttendanceDTOService service)
        {
            this.service = service;
        }


        public async Task<IndexViewModel> GetAttendancesIndexViewModelAsync()
        {
            IEnumerable<AttendanceDTO> attendances = await service.GetAllAsync(null, null, null, null, a => a.Day, a => a.Student );
            IndexViewModel viewModel = new IndexViewModel
            {
                Attendances = attendances,
                AttendanceModel = new Attendance(),
                StudentModel = new Student()
            };
            return viewModel;
        }
        public async Task<DetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await service.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DetailsViewModel viewModel = new DetailsViewModel
            {
                Attendance = attendance
            };
            return viewModel;
        }
        public async Task<EditViewModel> GetEditAttendanceViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await service.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            EditViewModel viewModel = new EditViewModel
            {
                Come = attendance.Come,
                Left = attendance.Left,
                Id = attendance.Id
            };
            return viewModel;
        }
        public async Task UpdateAsync(EditViewModel inputModel)
        {
            AttendanceDTO updatedAttendance = new AttendanceDTO
            {
                Id = inputModel.Id,
                Left = inputModel.Left,
                Come = inputModel.Come
            };
            service.Update(updatedAttendance, e => e.Left, e => e.Come);
            await service.SaveChangesAsync();
        }
        public async Task<DeleteViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await service.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Attendance = attendance
            };
            return viewModel;
        }
        public async Task DeleteAsync(DeleteInputModel inputModel)
        {
            AttendanceDTO attendanceToRemove = new AttendanceDTO { Id = inputModel.Id };
            service.Delete(attendanceToRemove);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }

    }
}