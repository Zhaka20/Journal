using System;
using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Controller.Assignments;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class AssignmentDTOBuilderData
    {
        private AssignmentFileDTO assignmentFile;
        private CreateViewModel inputModel;
        private EdtiViewModel inputModel1;
        private string mentorId;

        public AssignmentDTOBuilderData(EdtiViewModel inputModel1)
        {
            this.inputModel1 = inputModel1;
        }

        public AssignmentDTOBuilderData(CreateViewModel inputModel, string mentorId, AssignmentFileDTO assignmentFile)
        {
            this.inputModel = inputModel;
            this.mentorId = mentorId;
            this.assignmentFile = assignmentFile;
        }
    }
}
