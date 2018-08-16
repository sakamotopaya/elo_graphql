using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Elo
{
    public static class EfCoreUtilities
    {
        public static async Task<List<T>> PagedResults<T>(this DbSet<T> dbSet, int pageNumber, int pageSize) where T : class
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(25, pageSize);

            var skip = (pageNumber - 1) * pageSize;
            var take = Math.Min(100, Math.Max(0, pageSize));

            return await dbSet.Skip(skip).Take(take).ToListAsync();
        }
        public static async Task<List<T>> FilteredAndPagedResults<T>(this DbSet<T> dbSet,
                                                                            int pageNumber, int pageSize,
                                                                            Expression<Func<T, bool>> predicate) where T : class
        {
            if (pageNumber == 0)
                pageNumber = 1;

            if (pageSize == 0)
                pageSize = 25;

            var skip = (pageNumber - 1) * pageSize;
            var take = Math.Min(100, Math.Max(0, pageSize));

            return await dbSet.Where(predicate).Skip(skip).Take(take).ToListAsync();
        }
    }
}