using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Paging
{
    public class PagedResult<T>
    {
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }

        public PagedResult()
        {
        }

        public PagedResult(List<T> items, int count = 1, int pageIndex = 1, int pageSize = 10)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;

            Data = new List<T>(items);
        }


        public static PagedResult<T> CreateEmptyResult()
        {
            return new PagedResult<T>()
            {
                TotalPages = 1,
                PageIndex = 1,
                PageSize = 10
            };
        }

        public static async Task<PagedResult<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            var count = await source.CountAsync(cancellationToken);

            if (pageSize == null)
            {
                return new PagedResult<T>(source.ToList(), count, 1, count);
            }

            var items = await source.Skip((pageIndex - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync(cancellationToken);
            var data = new PagedResult<T>(items, count, pageIndex, pageSize.Value);

            return data;
        }


    }
}
