using Godot;
using System;

public class Card
{
    public string Name { get; set; }
    public ICardHolder AttachedHolder { get; set; }
}
