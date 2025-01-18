using Godot;
using System;

public class Card
{
    public string Name { get; init; }
    public string Emoji { get; init; }
    public ICardHolder AttachedHolder { get; set; }

    public Card(CardData data)
    {
        Name = data.Name;
        Emoji = data.Emoji;
    }

    public void Unattach()
    {
        AttachedHolder.Unattach(this);
    }

    public override string ToString()
    {
        return Name;
    }
}
