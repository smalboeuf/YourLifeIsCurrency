using System;
using System.Collections;
using System.Collections.Generic;

public static class Helpers
{
    private static Random random = new Random();

    public static T GetRandomListEntry<T>(this IList<T> source)
    {
        int index = random.Next(source.Count);
        return source[index];
    }
}
