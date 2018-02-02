using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Journal.ViewModels.Controller.Attendances;

namespace Journal.DTOFactory.Abstractions
{
    public interface IDTOFactory
    {
        TDTO CreateDTO<TDTO>();
        TDTO CreateDTO<TInput, TDTO>(TInput input);
    }
}