using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.CORE.Paging
{
    public class Paginate<T> : IPaginate<T>
    {
        //constructor
        public Paginate(IEnumerable<T> source, int index, int size, int from)
        {
            var enumerable = source as T[] ?? source.ToArray();

            if (from > index)
                throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

            if (source is IQueryable<T> querable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = querable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                Items = querable.Skip((Index - From) * Size).Take(Size).ToList();
            }
            else
            {
                Index = index-1;//ilk sayfayı atladığı için bir çıkardım
                Size = size; 
                From = from;

                Count = enumerable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
            }
        }

        public Paginate()
        {
            Items = new T[0];
        }

        public int From { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPrevious => Index - From > 0;
        public bool HasNext => Index - From + 1 < Pages;
    }

    
}
