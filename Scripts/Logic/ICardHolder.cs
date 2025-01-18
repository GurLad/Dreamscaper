using Godot;
using System;

public interface ICardHolder
{
    public bool IsFull { get; }
    public void Attach(Card card);
    public void Unattach(Card card);
}
