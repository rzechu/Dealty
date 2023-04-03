using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Dealty.Shared.UnitTests")]
[assembly: InternalsVisibleTo("Dealty.Shared.UnitTests.Filters")]
namespace Dealty.Shared.Filters
{
    public class PaginationFilter
    {
        internal const int MAX_PAGE_SIZE = 3;
        internal const int FIRST_PAGE_NUMBER = 1;
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public int Offset { get; private set; }
        public PaginationFilter()
        {
            PageSize = MAX_PAGE_SIZE;
            PageNumber = 1;
            Offset = PageSize * (PageNumber - 1);
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageSize = (pageSize > MAX_PAGE_SIZE || pageSize <= 0 ) ? MAX_PAGE_SIZE : pageSize;
            PageNumber = pageNumber <= 0 ? FIRST_PAGE_NUMBER : pageNumber;
            Offset = PageSize * (PageNumber - 1);
        }
    }
}