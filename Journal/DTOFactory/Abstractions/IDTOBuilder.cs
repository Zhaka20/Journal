using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journal.DTOFactory.Abstractions
{

    public interface IDTOBuilder<TDTO>
    {
        TDTO Build();
    }

    public interface IDTOBuilder<TInput, TDTO>
    {
        TDTO Build(TInput input);
    }

}