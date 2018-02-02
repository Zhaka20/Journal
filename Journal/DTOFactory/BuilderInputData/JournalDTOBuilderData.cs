using System.Collections.Generic;
using Journal.ViewModels.Controller.Journals;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class JournalDTOBuilderData
    {
        private EditViewModel viewModel;
        private CreateViewModel viewModel1;

        public JournalDTOBuilderData(CreateViewModel viewModel1)
        {
            this.viewModel1 = viewModel1;
        }

        public JournalDTOBuilderData(EditViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
