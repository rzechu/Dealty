using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealty.Shared.Filters;
using Xunit;

namespace Dealty.Shared.UnitTests.Filters
{
    public class PaginationFilterTest
    {
        public PaginationFilterTest()
        {

        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(13, 2)]
        [InlineData(15, 3)]
        public void Constructor_NormalValues(int pageNumber, int pageSize)
        {
            var paginationFilter = new PaginationFilter(pageNumber, pageSize);
            Assert.Equal(paginationFilter.PageSize, pageSize);
            Assert.Equal(paginationFilter.PageNumber, pageNumber);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(1132)]
        public void Constructor_DefaultValuesForInputOutOfRange(int pageSize)
        {
            var paginationFilter = new PaginationFilter(1, pageSize);

            Assert.Equal(PaginationFilter.MAX_PAGE_SIZE, paginationFilter.PageSize);
        }

        [Fact]
        public void Constructor_AboveMaxSize()
        {
            int lowerValue = PaginationFilter.MAX_PAGE_SIZE + 10;
            var paginationFilter = new PaginationFilter(1, lowerValue);

            Assert.Equal(PaginationFilter.MAX_PAGE_SIZE, paginationFilter.PageSize);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 30, 0)]
        [InlineData(0, -2, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(2, 0, PaginationFilter.MAX_PAGE_SIZE)]
        [InlineData(0, 500, PaginationFilter.MAX_PAGE_SIZE*0)]
        [InlineData(1, 500, PaginationFilter.MAX_PAGE_SIZE*0)]
        [InlineData(4, 500, PaginationFilter.MAX_PAGE_SIZE*(4-1))]
        public void Constructor_OffsetCalculation(int pageNumber, int pageSize, int expectedOffset)
        {
            var paginationFilter = new PaginationFilter(pageNumber, pageSize);
            Assert.Equal(expectedOffset, paginationFilter.Offset);
        }
    }
}