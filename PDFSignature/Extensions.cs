using System;
using System.Collections.Generic;
using System.Linq;

namespace PDFSignature
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Divide<T>(this IEnumerable<T> list, int count)
        {
            List<List<T>> result = new();
            int size = list.Count() / count;

            // Add and extra space for the spare element
            int[] lengths = new int[count];
            for (int i = 0; i < count; i++)
            {
                int addOne = (i < list.Count() - (size * count)) ? 1 : 0;
                lengths[i] = size + addOne;
            }

            // Copy the values
            int offset = 0;
            for (int i = 0; i < count; i++)
            {
                result.Add(list.Skip(offset).Take(lengths[i]).ToList());
                offset += lengths[i];
            }
            return result;
        }

        public static IEnumerable<IEnumerable<T>> Clump<T>(this IEnumerable<T> array, int size)
        {
            for (var i = 0; i < array.Count() / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}
