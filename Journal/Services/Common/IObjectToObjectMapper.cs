using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journal.WEB.Services.Common
{
    public interface IObjectToObjectMapper
    {
        TReturn Map<TInput, TReturn>(TInput objectToMap);
    }
}