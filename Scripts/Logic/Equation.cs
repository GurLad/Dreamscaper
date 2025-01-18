using Godot;
using System;
using System.Collections.Generic;

public class Equation : ACardHolder
{
    public string Result
    {
        get
        {
            if (card1 == null || card2 == null) return null;
            return combinator.Combine(card1, card2);
        }
    }
    public override bool IsFull => Result != null;

    public event Action<string> OnCombined;

    private Card card1;
    private Card card2;
    private Combinator combinator;

    public Equation(Combinator combinator)
    {
        this.combinator = combinator;
    }

    public override void Attach(Card card) => Attach(card, null);

    public void Attach(Card card, bool? is1)
    {
        if (is1 ?? (card1 == null))
        {
            card1 = card;
        }
        else
        {
            card2 = card;
        }
        base.Attach(card);
        if (Result != null)
        {
            OnCombined?.Invoke(Result);
        }
    }

    public override void Unattach(Card card)
    {
        if (card1 == card)
        {
            card1 = null;
        }
        else if (card2 == card)
        {
            card2 = null;
        }
        else
        {
            GD.PrintErr("[Equation] : Unattaching an unattached card (alt)! " + card + ", " + this);
            return;
        }
        base.Unattach(card);
    }

    public List<Card> RemoveAllCards()
    {
        List<Card> cards = new List<Card>() { card1, card2 };
        Unattach(card1);
        Unattach(card2);
        return cards;
    }
}
