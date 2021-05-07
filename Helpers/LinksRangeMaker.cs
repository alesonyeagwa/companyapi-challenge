using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Helpers
{
    public static class LinksRangeMaker
    {
        public static List<object> Maker(int c, int m)
        {
            int current = c;
            int last = m;
            int delta = 4;
            int left = current - delta;
            int right = current + delta + 1;

            List<int> range = new List<int>();
            List<object> rangeWithDots = new List<object>();

            int l = 0;

            for (int i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var item in range)
            {
                if (l > 0)
                {
                    if (item - l == 2)
                    {
                        rangeWithDots.Add(l + 1);
                    }
                    else if (item - l != 1)
                    {
                        rangeWithDots.Add("...");
                    }
                }
                rangeWithDots.Add(item);
                l = item;
            }

            return rangeWithDots;
        }
    }
}
