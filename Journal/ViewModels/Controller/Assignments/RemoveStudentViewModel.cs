﻿using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.Assignments
{
    public class RemoveStudentViewModel
    {
        public AssignmentViewModel Assignment { get; set; }
        public StudentViewModel Student { get; set; }
    }
}