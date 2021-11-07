using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFSignature;

namespace UnitTests
{
    [TestClass]
    public class ReorderTest
    {
        [TestMethod]
        public void TestReverse()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] expected = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            int[] actual = Reorder.Reverse(input).ToArray();
            Console.WriteLine("[{0}]", string.Join(", ", actual));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGroupedReverse()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] expected = new int[] { 4, 3, 2, 1, 8, 7, 6, 5 };
            int[] actual = Reorder.ReverseByGroup(input, 2).ToArray();
            Console.WriteLine("[{0}]", string.Join(", ", actual));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUnevenGroupedReverse()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            int[] expected = new int[] { 4, 3, 2, 1, 8, 7, 6, 5, 11, 10, 9 };
            int[] actual = Reorder.ReverseByGroup(input, 3).ToArray();
            Console.WriteLine("[{0}]", string.Join(", ", actual));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSigOrder()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] expected = new int[] { 12, 1, 2, 11, 10, 3, 4, 9, 8, 5, 6, 7};
            int[] actual = Reorder.OrderBySignature(input).ToArray();
            Console.WriteLine("[{0}]", string.Join(", ", actual));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGroupedSigOrder()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] expected = new int[] { 8, 1, 2, 7, 6, 3, 4, 5, 12, 9, 10, 11 };
            int[] actual = Reorder.OrderBySigGroups(input, 2).ToArray();
            Console.WriteLine("[{0}]", string.Join(", ", actual));
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
