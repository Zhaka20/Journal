using System.Collections.Generic;
using Journal.ViewModels.Controller.Mentors;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class MentorDTOBuilderData : ApplicationUserDTOBuilderData
    {
        private CreateViewModel viewModel;
        private EditViewModel viewModel1;

        public MentorDTOBuilderData(EditViewModel viewModel1)
        {
            this.viewModel1 = viewModel1;
        }

        public MentorDTOBuilderData(CreateViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
