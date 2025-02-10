using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using LinesLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    namespace LinesLib.Tests;
    
    [TestClass]
    [TestSubject(typeof(LineSegment))]
    public class LineSegmentTest
    {
        [TestMethod]
        [DataRow(1, 2, 1, 2)]
        [DataRow(4, 4, 4, 4)]
        public void ConstructorTest(int start, int end, int expectedStart, int expectedEnd)
        {
            LineSegment lineSegment = new LineSegment(start, end);
            Assert.AreEqual(expectedStart, lineSegment.start);
            Assert.AreEqual(expectedEnd, lineSegment.end);
        }
    
        [TestMethod]
        [DataRow(1, 2, "(1, 2)")]
        public void ToStringTest(int start, int end, string expected)
        {
            LineSegment lineSegment = new LineSegment(start, end);
            Assert.AreEqual(expected, lineSegment.ToString());
        }
    
        [TestMethod]
        [DataRow(1, 2, 1, true)]
        [DataRow(1, 2, 2, true)]
        [DataRow(1, 2, 0, false)]
        [DataRow(1, 2, 3, false)]
        public void ContainsTest(int start, int end, int value, bool expected)
        {
            LineSegment lineSegment = new LineSegment(start, end);
            Assert.AreEqual(expected, lineSegment.Contains(value));
        }
    
        [TestMethod]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 1, 3, false)]
        public void ContainsTest2(int start1, int end1, int start2, int end2, bool expected)
        {
            LineSegment lineSegment1 = new LineSegment(start1, end1);
            LineSegment lineSegment2 = new LineSegment(start2, end2);
            Assert.AreEqual(expected, lineSegment1.Contains(lineSegment2));
        }
    
        [TestMethod]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 1, 3, false)]
        public void EqualsTest(int start1, int end1, int start2, int end2, bool expected)
        {
            LineSegment lineSegment1 = new LineSegment(start1, end1);
            LineSegment lineSegment2 = new LineSegment(start2, end2);
            Assert.AreEqual(expected, lineSegment1.Equals(lineSegment2));
        }
    
        [TestMethod]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 1, 3, false)]
        public void GetHashCodeTest(int start1, int end1, int start2, int end2, bool expected)
        {
            LineSegment lineSegment1 = new LineSegment(start1, end1);
            LineSegment lineSegment2 = new LineSegment(start2, end2);
            Assert.AreEqual(expected, lineSegment1.GetHashCode() == lineSegment2.GetHashCode());
        }
    
        [TestMethod]
        [DataRow(1, 2, 1, 3, 1, 2)]
        [DataRow(1, 2, 2, 3, 2, 2)]
        [DataRow(1, 2, 3, 4, null, null)]
        public void IntersectionTest(int start1, int end1, int start2, int end2, int? expectedStart, int? expectedEnd)
        {
            LineSegment lineSegment1 = new LineSegment(start1, end1);
            LineSegment lineSegment2 = new LineSegment(start2, end2);
            LineSegment expected = expectedStart.HasValue ? new LineSegment(expectedStart.Value, expectedEnd.Value) : null;
            Assert.AreEqual(expected, lineSegment1.Intersection(lineSegment2));
        }
    
        [TestMethod]
        [DataRow(1, 2, 3, 4, new[] { 1, 2, 3, 4 })]
        [DataRow(1, 3, 2, 4, new[] { 1, 4 })]
        [DataRow(1, 2, 2, 3, new[] { 1, 3 })]
        [DataRow(1, 4, 2, 3, new[] { 1, 4 })]
        public void UnionTest(int start1, int end1, int start2, int end2, int[] expected)
        {
            LineSegment lineSegment1 = new LineSegment(start1, end1);
            LineSegment lineSegment2 = new LineSegment(start2, end2);
            List<LineSegment> result = lineSegment1.Union(lineSegment2);
            List<LineSegment> expectedList = new List<LineSegment>();
            for (int i = 0; i < expected.Length; i += 2)
            {
                expectedList.Add(new LineSegment(expected[i], expected[i + 1]));
            }
            CollectionAssert.AreEqual(expectedList, result);
        }
    
        [TestMethod]
        [DataRow(1, 4, 2, 3, new[] { 1, 1, 4, 4 })]
        [DataRow(2, 3, 1, 4, new int[] { })]
        [DataRow(1, 3, 2, 4, new[] { 1, 1 })]
        [DataRow(1, 2, 3, 4, new[] { 1, 2 })]
        [DataRow(1, 2, 2, 3, new[] { 1, 1 })]
        public void MinusTest(int start1, int end1, int start2, int end2, int[] expected)
        {
            LineSegment lineSegment1 = new LineSegment(start1, end1);
            LineSegment lineSegment2 = new LineSegment(start2, end2);
            List<LineSegment> result = lineSegment1.Minus(lineSegment2);
            List<LineSegment> expectedList = new List<LineSegment>();
            for (int i = 0; i < expected.Length; i += 2)
            {
                expectedList.Add(new LineSegment(expected[i], expected[i + 1]));
            }
            CollectionAssert.AreEqual(expectedList, result);
        }
    }