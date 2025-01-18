using Godot;
using System;

public partial class LevelController : Node
{
    [Export] private UIDeck deck;

    private Level level;

    public override void _Ready()
    {
        base._Ready();

    }
}
