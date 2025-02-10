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
    public void ConstructorTest()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        Assert.AreEqual(1, lineSegment.start);
        Assert.AreEqual(2, lineSegment.end);
        LineSegment lineSegment2 = new LineSegment(4, 4);
        Assert.ThrowsException<ArgumentException>(() => new LineSegment(2, 1));
    }
    
    [TestMethod]
    public void ToStringTest()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        Assert.AreEqual("(1, 2)", lineSegment.ToString());
    }
    
    [TestMethod]
    public void ContainsTest()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        Assert.IsTrue(lineSegment.Contains(1));
        Assert.IsTrue(lineSegment.Contains(2));
        Assert.IsFalse(lineSegment.Contains(0));
        Assert.IsFalse(lineSegment.Contains(3));
        LineSegment lineSegment2 = new LineSegment(1, 2);
        Assert.IsTrue(lineSegment.Contains(lineSegment2));
        LineSegment lineSegment3 = new LineSegment(1, 3);
        Assert.IsFalse(lineSegment.Contains(lineSegment3));
    }
    
    [TestMethod]
    public void ContainsTest2()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(1, 2);
        Assert.IsTrue(lineSegment.Contains(lineSegment2));
        LineSegment lineSegment3 = new LineSegment(1, 3);
        Assert.IsFalse(lineSegment.Contains(lineSegment3));
    }
    
    [TestMethod]
    public void EqualsTest()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(1, 2);
        Assert.AreEqual(lineSegment, lineSegment2);
        
        LineSegment lineSegment3 = new LineSegment(1, 3);
        Assert.AreNotEqual(lineSegment, lineSegment3);
        
        Assert.AreNotEqual(lineSegment, null);
        
        Assert.AreNotEqual<object>(lineSegment, "not a line segment");
    }
    
    [TestMethod]
    public void GetHashCodeTest()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(1, 2);
        Assert.AreEqual(lineSegment.GetHashCode(), lineSegment2.GetHashCode());
        LineSegment lineSegment3 = new LineSegment(1, 3);
        Assert.AreNotEqual(lineSegment.GetHashCode(), lineSegment3.GetHashCode());
    }
    
    [TestMethod]
    public void IntersectionTest()
    {
        LineSegment lineSegment = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(1, 3);
        LineSegment lineSegment3 = new LineSegment(2, 3);
        LineSegment lineSegment4 = new LineSegment(3, 4);
        Assert.AreEqual(new LineSegment(1, 2), lineSegment.Intersection(lineSegment2));
        Assert.AreEqual(new LineSegment(2, 2), lineSegment.Intersection(lineSegment3));
        Assert.IsNull(lineSegment.Intersection(lineSegment4));
    }
    
    [TestMethod]
    public void UnionTest_NonOverlappingSegments()
    {
        LineSegment lineSegment1 = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(3, 4);
        List<LineSegment> result = lineSegment1.Union(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 2), new LineSegment(3, 4) }, result);
    }
    
    [TestMethod]
    public void UnionTest_OverlappingSegments()
    {
        LineSegment lineSegment1 = new LineSegment(1, 3);
        LineSegment lineSegment2 = new LineSegment(2, 4);
        List<LineSegment> result = lineSegment1.Union(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 4) }, result);
    }
    
    [TestMethod]
    public void UnionTest_AdjacentSegments()
    {
        LineSegment lineSegment1 = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(2, 3);
        List<LineSegment> result = lineSegment1.Union(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 3) }, result);
    }
    
    [TestMethod]
    public void UnionTest_ContainedSegment()
    {
        LineSegment lineSegment1 = new LineSegment(1, 4);
        LineSegment lineSegment2 = new LineSegment(2, 3);
        List<LineSegment> result = lineSegment1.Union(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 4) }, result);
    }
    
    [TestMethod]
    public void MinusTest()
    {
        LineSegment lineSegment1 = new LineSegment(1, 4);
        LineSegment lineSegment2 = new LineSegment(2, 3);
        List<LineSegment> result = lineSegment1.Minus(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 1), new LineSegment(4, 4) }, result);
    }
    
    [TestMethod]
    public void MinusTest_ContainedSegment()
    {
        LineSegment lineSegment1 = new LineSegment(1, 4);
        LineSegment lineSegment2 = new LineSegment(2, 3);
        List<LineSegment> result = lineSegment2.Minus(lineSegment1);
        CollectionAssert.AreEqual(new List<LineSegment>(), result);
    }
    
    [TestMethod]
    public void MinusTest_OverlappingSegments()
    {
        LineSegment lineSegment1 = new LineSegment(1, 3);
        LineSegment lineSegment2 = new LineSegment(2, 4);
        List<LineSegment> result = lineSegment1.Minus(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 1) }, result);
    }
    
    [TestMethod]
    public void MinusTest_NonOverlappingSegments()
    {
        LineSegment lineSegment1 = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(3, 4);
        List<LineSegment> result = lineSegment1.Minus(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 2) }, result);
    }
    
    [TestMethod]
    public void MinusTest_AdjacentSegments()
    {
        LineSegment lineSegment1 = new LineSegment(1, 2);
        LineSegment lineSegment2 = new LineSegment(2, 3);
        List<LineSegment> result = lineSegment1.Minus(lineSegment2);
        CollectionAssert.AreEqual(new List<LineSegment> { new LineSegment(1, 1) }, result);
    }
}