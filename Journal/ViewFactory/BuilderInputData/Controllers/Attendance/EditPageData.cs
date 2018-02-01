using System;
using System.ComponentModel.DataAnnotations;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance
{
    public class EditPageData
    {
        public AttendanceDTO Attendance { get; internal set; }
    }
}