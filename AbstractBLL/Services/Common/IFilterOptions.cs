using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractBLL.Services.Common
{
    public abstract class AbstractFilterOptions<TKey>
    {
        IList<string> IncludeProperties { get; set; }
        IList<Dictionary<string,SortOrder>> OrderBy { get; set; }
        bool FinById { get; set; }
        TKey Id { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }
}
