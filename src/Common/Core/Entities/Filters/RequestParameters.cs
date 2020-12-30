using System;

namespace eShopWithReact.Common.Core.Entities.Filters
{
    public class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageSize { get; set; }
        public int PageNumber { get; set; } = 1;

        public RequestParameters()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public RequestParameters(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = (pageSize > maxPageSize) ? maxPageSize : pageSize;
        }
    }
}
