using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class CardData
{
    public string Name { get; init; }
    public string Emoji { get; init; }

    public CardData(string name, string emoji)
    {
        Name = name;
        Emoji = emoji;
    }
}

public static class CardDataHolder
{
    public static List<CardData> Cards { get; } = new List<CardData>();

    private static string rawData = """
    Falling,⬇️
    Water,💦
    Human,🧑
    """;

    public static void Init()
    {
        List<string> entries = rawData.ToLineBrokenList();
        entries.RemoveAll(a => !a.Contains(","));
        entries.ForEach(a => 
        {
            string[] parts = a.Split(",");
            Cards.Add(new CardData(parts[0], parts[1]));
        });
    }
}
