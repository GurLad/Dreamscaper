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
    private static List<CardData> Cards { get; } = new List<CardData>();

    public static Card GetCard(string s) => new Card(GetData(s));

    public static CardData GetData(string s) => Cards.Find(a => a.Name == s);

    private static string rawData = """
    Falling,⬇️
    Rain,💦
    Human,🧑
    Bullet,⚫
    Time,⏳
    Machine,⚙️
    Wound,🩹
    Flying,🥏
    Stone,🪨
    Bird,🐦
    Huge,⤴️
    Rock,🪨
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
