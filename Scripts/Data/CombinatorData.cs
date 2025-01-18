using Godot;
using System;
using System.Collections.Generic;

public class CombinatorData
{
    public Dictionary<(string, string), string> Dictionary { get; init; }

    public CombinatorData(Dictionary<(string, string), string> dictionary)
    {
        Dictionary = dictionary;
    }
}

public static class CombinatorDataHolder
{
    public static CombinatorData Data { get; private set; }

    private static string rawData = """
    Falling,Falling,Emptiness
    Falling,Rain,Calm
    Falling,Human,Scary
    Rain,Rain,Storm
    Rain,Human,Nostalgic
    Human,Human,Love
    Bullet,Bullet,War
    Time,Time,Peace
    Time,Wound,Peace
    Bullet,Time,Exciting
    Bullet,Wound,Exciting
    Time,Machine,Exciting
    Falling,Bird,Scary
    Flying,Stone,Scary
    Flying,Bird,Normal
    Flying,Falling,Scary
    Falling,Stone,Scary
    """;

    public static void Init()
    {
        Dictionary<(string, string), string> result = new Dictionary<(string, string), string>();
        List<string> entries = rawData.ToLineBrokenList();
        entries.RemoveAll(a => !a.Contains(","));
        entries.ForEach(a =>
        {
            string[] parts = a.Split(",");
            result.Add((parts[0], parts[1]), parts[2]);
        });
        Data = new CombinatorData(result);
    }
}
