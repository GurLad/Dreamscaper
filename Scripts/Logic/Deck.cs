using Godot;
using System;
using System.Collections.Generic;

public class Deck : ACardHolder
{
    public override bool IsFull => false;

    private List<Card> cards;

    public override void Attach(Card card)
    {
        base.Attach(card);
        cards.Add(card);
    }

    public override void Unattach(Card card)
    {
        base.Unattach(card);
        cards.Remove(card);
    }
}
