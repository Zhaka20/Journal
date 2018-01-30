﻿using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.DataModel.Models;
using Journal.AbstractBLL.AbstractServices;
using System;
using Journal.ViewModels.Controller.Attendances;

namespace Journal.Services.ControllerServices
{
    public class AttendancesControllerService : IAttendancesControllerService
    {
        protected readonly IAttendanceService service;
        public AttendancesControllerService(IAttendanceService service)
        {
            this.service = service;
        }


        public async Task<IndexViewModel> GetAttendancesIndexViewModelAsync()
        {
            IEnumerable<Attendance> attendances = await service.GetAllAsync(null, null, null, null, a => a.Day, a => a.Student );
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
            Attendance attendance = await service.GetByIdAsync(attendanceId);
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
            Attendance attendance = await service.GetByIdAsync(attendanceId);
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
            Attendance updatedAttendance = new Attendance
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
            Attendance attendance = await service.GetByIdAsync(attendanceId);
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
            Attendance attendanceToRemove = new Attendance { Id = inputModel.Id };
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