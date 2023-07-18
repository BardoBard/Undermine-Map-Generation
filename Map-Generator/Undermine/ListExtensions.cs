using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map_Generator.Undermine;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        while (count > 1)
        {
            int index = Rand.Range(0, count--);
            (list[index], list[count]) = (list[count], list[index]);
        }
    }

    public static void Shuffle<T>(this IList<T> list, int seed)
    {
        System.Random random = new System.Random(seed);
        int count = list.Count;
        while (count > 1)
        {
            int index = random.Next(0, count--);
            (list[index], list[count]) = (list[count], list[index]);
        }
    }

    public static void CopyTo<T>(this List<T> list, List<T> destination)
    {
        destination.Clear();
        destination.AddRange(list);
    }
}