using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Domain
{
    public class SearchParams<T>
    {
        public int ItemsToReturn { get; set; } = 1;
        public int ItemsToSkip { get; set; } = 0;
        public Expression<Func<T,bool>> ? Query { get; set; }
    }
}
