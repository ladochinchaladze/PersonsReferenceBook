using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Paging
{
    public class FilterBaseQuery<TFilter, TResult> : IRequest<TResult>
    {
        public int? PageSize { get; set; }
        public int PageIndex { get; set; } = 1;
        public TFilter Filter { get; set; }
        public Sorting Sorting { get; set; }
    }
}
