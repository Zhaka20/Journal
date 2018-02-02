using System;
using Journal.ViewModels.Controller.Attendances;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class AttendanceDTOBuilderData
    {
        private EditViewModel inputModel;
        private DeleteInputModel inputModel1;

        public AttendanceDTOBuilderData(DeleteInputModel inputModel1)
        {
            this.inputModel1 = inputModel1;
        }

        public AttendanceDTOBuilderData(EditViewModel inputModel)
        {
            this.inputModel = inputModel;
        }
    }
}
