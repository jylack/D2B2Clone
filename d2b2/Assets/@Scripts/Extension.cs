using System;

public static class Extension
{
    public static void ForEach(this Range range, Action<int> act)
    {
        for (int i = range.Start.Value; i < range.End.Value; i++)
            act?.Invoke(i);
    }
}