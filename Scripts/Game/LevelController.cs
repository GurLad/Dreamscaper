using Godot;
using System;

public partial class LevelController : Node
{
    [Export] private UIDeck deck;
    [Export] private UIEquation equation;

    private Level level;

    private static int levelNumber;

    public override void _Ready()
    {
        base._Ready();
        level = LevelDataHolder.GetLevel(levelNumber);
        deck.Init(level.Cards);
        equation.Init(level.Equation);
    }
}
