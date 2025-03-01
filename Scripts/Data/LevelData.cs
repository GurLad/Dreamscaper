using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelData
{
    public List<string> Cards { get; init; }
    public List<string> Targets { get; init; }

    public LevelData(List<string> cards, List<string> targets)
    {
        Cards = cards;
        Targets = targets;
    }
}

public static class LevelDataHolder
{
    private static List<LevelData> Levels { get; } = new List<LevelData>();

    public static Level GetLevel(int id) => new Level(GetData(id));

    public static LevelData GetData(int id) => Levels[id];

    public static int Count => Levels.Count;

    private static string[] rawDatas =
    {
        """
        Falling
        Falling
        Human
        Human
        Rain
        Rain
        ~
        Calm
        Scary
        Nostalgic
        """,
        """
        Bullet
        Bullet
        Time
        Time
        Machine
        Wound
        ~
        Exciting
        Exciting
        Exciting
        """,
        """
        Flying
        Bird
        Stone
        Falling
        ~
        Scary
        Scary
        """,
    };

    public static void Init()
    {
        rawDatas.ToList().ForEach(rawData =>
        {
            List<string> entries = rawData.ToLineBrokenList();
            bool readingCards = true;
            List<string> cards = new List<string>();
            List<string> targets = new List<string>();
            entries.ForEach(a =>
            {
                if (a == "~")
                {
                    readingCards = false;
                    return;
                }
                if (readingCards)
                {
                    cards.Add(a);
                }
                else
                {
                    targets.Add(a);
                }
            });
            Levels.Add(new LevelData(cards, targets));
        });
    }
}

