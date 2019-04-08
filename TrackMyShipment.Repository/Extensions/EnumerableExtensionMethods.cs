using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyShipment.Repository.Extensions
{
    public static class EnumerableExtensionMethods
    {
        public static async Task<IEnumerable<T>> WhereAsync<T>(this IEnumerable<T> source, Func<T, bool> selector)
        {
            return await Task.Run(() => source.Where(selector));
        }
    }
}