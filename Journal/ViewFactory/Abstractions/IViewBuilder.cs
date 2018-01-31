using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.ViewFactory.Abstractions
{
    public interface IViewBuilder<TView>
    {
        TView Build();
    }

    public interface IViewBuilder<TInput, TView>
    {
        TView Build(TInput input);
    }
}
