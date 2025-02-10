namespace LinesLib;

public class LineSegment
{
    public int start { get; }
    public int end { get; }
    
    public LineSegment(int start, int end)
    {
        this.start = start;
        this.end = end;
        if (start > end)
        {
            throw new ArgumentException("Start must be less than or equal to end");
        }
    }
    
    public override string ToString()
    {
        return $"({start}, {end})";
    }
    
    public bool Contains(int punkt)
    {
        return punkt >= start && punkt <= end;
    }
    
    public bool Contains(LineSegment lineSegment)
    {
        return Contains(lineSegment.start) && Contains(lineSegment.end);
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is LineSegment lineSegment)
        {
            return start == lineSegment.start && end == lineSegment.end;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(start, end);
    }
    
    public LineSegment Intersection(LineSegment anotherSegment)
    {
        int newStart = Math.Max(start, anotherSegment.start);
        int newEnd = Math.Min(end, anotherSegment.end);
        if (newStart > newEnd)
        {
            return null;
        }
        return new LineSegment(newStart, newEnd);
    }
    
    public List<LineSegment>? Union(LineSegment anotherSegment)
    {
        List<LineSegment> segments = new List<LineSegment>();
        if(end < anotherSegment.start || anotherSegment.end < start)
        {
            segments.Add(new LineSegment(start, end));
            segments.Add(anotherSegment);
            return segments;
        }
        return new List<LineSegment> { new LineSegment(Math.Min(start, anotherSegment.start), Math.Max(end, anotherSegment.end)) };
    }
    
    public List<LineSegment>? Minus(LineSegment anotherSegment)
    {
        if (anotherSegment.start <= start && anotherSegment.end >= end)
        {
            return new List<LineSegment>();
        }
        if (anotherSegment.start > end || anotherSegment.end < start)
        {
            return new List<LineSegment> { new LineSegment(start, end) };
        }
        List<LineSegment> segments = new List<LineSegment>();
        if (anotherSegment.start > start)
        {
            segments.Add(new LineSegment(start, anotherSegment.start - 1));
        }
        if (anotherSegment.end < end)
        {
            segments.Add(new LineSegment(anotherSegment.end + 1, end));
        }
        return segments;
    }
}