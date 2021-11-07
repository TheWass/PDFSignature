using System;
using System.Collections.Generic;
using System.Linq;

namespace PDFSignature
{
    public static class Reorder
    {
        public static IEnumerable<int> Reverse(IEnumerable<int> input)
        {
            return input.Reverse();
        }

        public static IEnumerable<int> ReverseByGroup(IEnumerable<int> input, int groupCount)
        {
            return input.Divide(groupCount).SelectMany(list => list.Reverse());
        }

        public static IEnumerable<int> OrderBySignature(IEnumerable<int> input)
        {
            if (input.Count() % 4 != 0)
            {
                throw new ArgumentException("Invalid count.  Must be %4.");
            }
            var values = input.ToArray();
            List<int> result = new();
            int length = input.Count();
            for (int i = 0; i < length; i++)
            {
                int j = (int)Math.Floor(i / 4.0);
                switch (i%4)
                {
                    case 3:
                        result.Add(values[length - 2 - 2 * j]);
                        break;
                    case 2:
                        result.Add(values[1 + 2 * j]);
                        break;
                    case 1:
                        result.Add(values[2 * j]);
                        break;
                    case 0:
                        result.Add(values[length - 1 - 2 * j]);
                        break;
                }
            }
            return result;
        }

        public static IEnumerable<int> OrderBySigGroups(IEnumerable<int> input, int groupCount)
        {
            var stage0 = input;
            var stage1 = stage0.Clump(4);
            var stage2 = stage1.Divide(groupCount);
            var stage3 = stage2.Select(l => l.SelectMany(l => l));
            var stage4 = stage3.SelectMany(OrderBySignature);
            return stage4;
        }
    }
}