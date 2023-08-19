using System;
using Map_Generator.Unity;

namespace Map_Generator.Undermine;

public static class Whip
{
    private const float WhipCycleHours = 24f;

    private static readonly DateTime WhipEpoch = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static DateTime _serverTimeUtc = DateTime.UtcNow;

    private static int CurrentWhipCycle => (int)System.Math.Floor(_serverTimeUtc.Subtract(WhipEpoch).TotalHours / WhipCycleHours);

    public static int CurrentWhipSeed => Rand.ClampSeed(Mathf.Abs($"whip_{CurrentWhipCycle}_seed".MyGetHashCode()));
}