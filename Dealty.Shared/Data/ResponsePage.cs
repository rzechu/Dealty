using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealty.Shared.Filters;

namespace Dealty.Shared.Data
{
    public class ResponsePage<T>
    {
        public IEnumerable<T> Data { get;  set; }
        public int CurrentPage { get;  set; }
        public int TotalPages { get;  set; }
        public int PageSize { get;  set; }
        public int TotalCount { get;  set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public ResponsePage()
        {
            
        }
        public ResponsePage(List<T> items, int pageNumber, int pageSize, int totalCount)
        {
            int count = items.Count;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            //Data.AddRange(items);
            Data = items;
        }

        public ResponsePage(IEnumerable<T> source, PaginationFilter paginationFilter, int totalCount) 
            : this (source.ToList(), paginationFilter.PageNumber, paginationFilter.PageSize, totalCount)
        {
        }
    }
}