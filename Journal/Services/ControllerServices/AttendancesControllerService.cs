using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Journal.ViewModels.Controller.Attendances;
using Journal.BLLtoUIData.DTOs;
using Journal.AbstractBLL.AbstractServices;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance;

namespace Journal.Services.ControllerServices
{
    public class AttendancesControllerService : IAttendancesControllerService
    {
        protected readonly IAttendanceDTOService attendanceService;
        protected readonly IViewFactory viewFactory;

        public AttendancesControllerService(IAttendanceDTOService attendanceService, IViewFactory viewFactory)
        {
            this.viewFactory = viewFactory;
            this.attendanceService = attendanceService;
        }

        public async Task<IndexViewModel> GetAttendancesIndexViewModelAsync()
        {
            IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAllAsync(null, null, null, null, a => a.Day, a => a.Student );
            var pageData = new IndexPageData
            {
                Attendances = attendances
            };
            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData,IndexViewModel>(pageData);
            return viewModel;
        }
        public async Task<DetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await attendanceService.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DetailsPageData pageData = new DetailsPageData
            {
                Attendance = attendance
            };
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(pageData);
            return viewModel;
        }
        public async Task<EditViewModel> GetEditAttendanceViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await attendanceService.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            EditPageData pageData = new EditPageData
            {
                Attendance = attendance
            };
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(pageData);
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
            attendanceService.Update(updatedAttendance, e => e.Left, e => e.Come);
            await attendanceService.SaveChangesAsync();
        }
        public async Task<DeleteViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await attendanceService.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DeletePageData pageData = new DeletePageData
            {
                Attendance = attendance
            };
            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(pageData);
            return viewModel;
        }
        public async Task DeleteAsync(DeleteInputModel inputModel)
        {
            AttendanceDTO attendanceToRemove = new AttendanceDTO { Id = inputModel.Id };
            attendanceService.Delete(attendanceToRemove);
            await attendanceService.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = attendanceService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }

    }
}