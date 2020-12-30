using eShopWithReact.Common.Core.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eShopWithReact.Common.Core.Entities.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedResponse(T data, long count, RequestParameters filter)
        {
            Data = data;
            TotalCount = count;
            PageSize = filter.PageSize;
            CurrentPage = filter.PageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)filter.PageSize);
            Message = null;
            Succeeded = true;
            Errors = null;
        }
    }
}
