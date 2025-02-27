using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MantenimientoCrud.Models
{
    public class PaginatedLists<T> : List<T>
    {
        public PaginatedLists(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            this.AddRange(items);
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }

        public int TotalCount { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}