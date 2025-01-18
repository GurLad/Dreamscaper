using Godot;
using System;
using System.Collections.Generic;

public partial class UIDeck : Control
{
    [Export] private PackedScene sceneUICardHolder;
    [Export] private Container container;

    private Deck deck;

    public void Init(List<Card> cards)
    {
        deck = new Deck();
        cards.ForEach(a => 
        {
            UICardHolder holder = sceneUICardHolder.Instantiate<UICardHolder>();
            holder.Init(a);
            container.AddChild(holder);
            deck.Attach(a);
            holder.OnCardDropped += a => deck.Attach(a.Card);
            holder.OnCardUnattached += a => deck.Unattach(a.Card);
        });
    }
}
