using Godot;
using System;

public partial class LevelController : Node
{
    [Export] private UIDeck deck;
    [Export] private UIEquation equation;
    [Export] private UIGoal goal;
    [ExportCategory("Sound")]
    [Export] private AudioStream retrySFX;

    private Level level;

    private static int levelNumber = 0;

    public override void _Ready()
    {
        base._Ready();
        level = LevelDataHolder.GetLevel(levelNumber);
        deck.Init(level.Cards);
        equation.Init(level, level.Equation);
        goal.Init(level);
        goal.OnFinishAnimationAll += NextLevel;
    }

    public void NextLevel()
    {
        levelNumber++;
        SceneController.Current.TransitionToScene(levelNumber >= LevelDataHolder.Count ? "Win" : "Game");
    }

    public void RetryLevel()
    {
        SceneController.Current.TransitionToScene("Game");
        SoundController.Current.PlaySFX(retrySFX);
    }
}
