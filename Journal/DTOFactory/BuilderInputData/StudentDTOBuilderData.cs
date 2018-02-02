using System.Collections.Generic;
using Journal.ViewModels.Controller.Students;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class StudentDTOBuilderData : ApplicationUserDTOBuilderData
    {
        private CreateViewModel viewModel;
        private EditViewModel viewModel1;

        public StudentDTOBuilderData(EditViewModel viewModel1)
        {
            this.viewModel1 = viewModel1;
        }

        public StudentDTOBuilderData(CreateViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
