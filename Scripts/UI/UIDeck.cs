using Godot;
using System;
using System.Collections.Generic;

public partial class UIDeck : Control
{
    [Export] private PackedScene sceneUICardHolder;
    [Export] private Container container;

    public void Init(List<Card> cards)
    {
        cards.ForEach(a => 
        {
            UICardHolder holder = sceneUICardHolder.Instantiate<UICardHolder>();
            holder.Init(a);
            container.AddChild(holder);
        });
    }
}
