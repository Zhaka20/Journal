using System;
using System.Collections.Generic;
using Journal.ViewModels.Controller.WorkDays;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class WorkDayDTOBuilderData
    {
        private EditViewModel inputModel1;

        public CreateViewModel inputModel { get; private set; }

        public WorkDayDTOBuilderData(CreateViewModel inputModel)
        {
            this.inputModel = inputModel;
        }

        public WorkDayDTOBuilderData(EditViewModel inputModel1)
        {
            this.inputModel1 = inputModel1;
        }
    }
}
