using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public PaginatedList(List<T> items, int count, int page, int perPage)
        {
            TotalItems = count;
            Page = page;
            PerPage = perPage;
            TotalPages = (int)Math.Ceiling(count / (double)perPage);

            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source,
        int page, int perPage)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * perPage).Take(perPage).ToListAsync();
            return new PaginatedList<T>(items, count, page, perPage);
        }
    }
}