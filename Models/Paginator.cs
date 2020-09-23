using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Models
{

    public class Paginator
    {
        public int CPage { get; }
        public int Count { get; }
        public int Size { get; }
        public int Skip { get; }
        public int Take { get; }
        public int TotalPages { get; }
        public int FirstPage { get; }
        public int LastPage { get; }
        public int PrevPage { get; }
        public int NextPage { get; }


        public Paginator(int cpage, int size, int count)
        {
            Count = count;
            Size = size;
            CPage = cpage;

            Skip = (CPage - 1) * Size;
            Take = Size;
            TotalPages = (int)Math.Ceiling(Decimal.Divide(Count, Size));            
            FirstPage = 1;

            if (CPage > TotalPages) { CPage = 1; }

            LastPage = TotalPages < 1 ? 1 : TotalPages;
            PrevPage = Math.Max(CPage - 1, FirstPage);
            NextPage = Math.Min(CPage + 1, LastPage);

            
        }


    }
}
