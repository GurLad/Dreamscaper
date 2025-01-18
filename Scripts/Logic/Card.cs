using Godot;
using System;

public class Card
{
    public string Name { get; init; }
    public ICardHolder AttachedHolder { get; set; }

    public void Unattach()
    {
        AttachedHolder.Unattach(this);
    }

    public override string ToString()
    {
        return Name;
    }
}
