using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Journal.ViewModels.Controller.Attendances;
using Journal.BLLtoUIData.DTOs;
using Journal.AbstractBLL.AbstractServices;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance;
using Journal.DTOFactory.Abstractions;
using Journal.DTOBuilderDataFactory.BuilderInputData;

namespace Journal.Services.ControllerServices
{
    public class AttendancesControllerService : IAttendancesControllerService
    {
        protected readonly IAttendanceDTOService attendanceService;
        protected readonly IViewFactory viewFactory;
        protected readonly IDTOFactory dtoFactory;

        public AttendancesControllerService(IAttendanceDTOService attendanceService, IViewFactory viewFactory,IDTOFactory dtoFactory)
        {
            this.viewFactory = viewFactory;
            this.attendanceService = attendanceService;
            this.dtoFactory = dtoFactory;
        }

        public async Task<IndexViewModel> GetAttendancesIndexViewModelAsync()
        {
            IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAllAsyncWithDayAndStudents();
            var viewModelData = new IndexViewData(attendances);
            IndexViewModel viewModel = viewFactory.CreateView<IndexViewData,IndexViewModel>(viewModelData);
            return viewModel;
        }
        public async Task<DetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await attendanceService.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DetailsViewData viewModelData = new DetailsViewData(attendance);
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsViewData, DetailsViewModel>(viewModelData);
            return viewModel;
        }
        public async Task<EditViewModel> GetEditAttendanceViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await attendanceService.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            EditViewData viewModelData = new EditViewData(attendance);
            EditViewModel viewModel = viewFactory.CreateView<EditViewData, EditViewModel>(viewModelData);
            return viewModel;
        }
        public async Task UpdateAsync(EditViewModel inputModel)
        {
            AttendanceDTOBuilderData builderData = new AttendanceDTOBuilderData(inputModel);
            AttendanceDTO updatedAttendance = dtoFactory.CreateDTO<AttendanceDTOBuilderData, AttendanceDTO>(builderData);
            attendanceService.UpdateBaseInfo(updatedAttendance);
            await attendanceService.SaveChangesAsync();
        }
        public async Task<DeleteViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId)
        {
            AttendanceDTO attendance = await attendanceService.GetByIdAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DeleteViewData viewModelData = new DeleteViewData(attendance);
            DeleteViewModel viewModel = viewFactory.CreateView<DeleteViewData, DeleteViewModel>(viewModelData);
            return viewModel;
        }
        public async Task DeleteAsync(DeleteInputModel inputModel)
        {
            AttendanceDTOBuilderData bulderData = new AttendanceDTOBuilderData(inputModel);
            AttendanceDTO attendanceToRemove = dtoFactory.CreateDTO<AttendanceDTOBuilderData, AttendanceDTO>(bulderData);
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