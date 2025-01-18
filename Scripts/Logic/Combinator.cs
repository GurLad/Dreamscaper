using Godot;
using System;

public static class Combinator
{
    private static CombinatorData data;

    public static string Combine(Card card1, Card card2)
    {
        if (data == null)
        {
            data = CombinatorDataHolder.Data;
        }
        if (data.Dictionary.ContainsKey((card1.Name, card2.Name)))
        {
            return data.Dictionary[(card1.Name, card2.Name)];
        }
        else if (data.Dictionary.ContainsKey((card2.Name, card1.Name)))
        {
            return data.Dictionary[(card2.Name, card1.Name)];
        }
        else
        {
            return "Surreal";
        }
    }
}
