using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BakPisir.CORE.Paging
{
    public static class IQueryablePaginateExtensions
    {
        public static IPaginate<T> ToPaginate<T>(this List<T> source, int index, int size, int from = 0)
        {

            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

            int count = source.Count();

            List<T> items = source;
            //Paginate<T> list = new Paginate<T>
            //{
            //    Index = index,
            //    Size = size,
            //    From = from,
            //    Count = count,
            //    Items = items,
            //    Pages = (int)Math.Ceiling(count / (double)size)

            //};
            Paginate<T> list = new Paginate<T>(source, index, size, from);
            return list;
        }

       
    }
}
