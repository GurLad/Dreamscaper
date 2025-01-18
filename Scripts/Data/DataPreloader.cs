using Godot;
using System;

public partial class DataPreloader : Node
{
    public override void _Ready()
    {
        base._Ready();
        CardDataHolder.Init();
        LevelDataHolder.Init();
        CombinatorDataHolder.Init();
    }
}
