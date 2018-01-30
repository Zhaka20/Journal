namespace BLL.Services.Common.Abstract
{
    public interface IObjectToObjectMapper
    {
        TReturn Map<TInput, TReturn>(TInput objectToMap);
    }
}

