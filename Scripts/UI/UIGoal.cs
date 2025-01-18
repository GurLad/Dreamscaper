using Godot;
using System;
using System.Collections.Generic;

public partial class UIGoal : Control
{
    [Export] private PackedScene sceneGoalPart;

    [Signal]
    public delegate void OnFinishAnimationAllEventHandler();

    private int count = 0;
    private List<UIGoalPart> goalParts = new List<UIGoalPart>();

    public void Init(Level level)
    {
        count = level.Targets.Count;
        level.Targets.ForEach(a =>
        {
            UIGoalPart part = sceneGoalPart.Instantiate<UIGoalPart>();
            part.Init(a);
            part.OnFinishAnimation += FinishedOne;
            goalParts.Add(part);
        });
        level.OnMatched += Match;
    }

    private void Match(string s)
    {
        UIGoalPart part = goalParts.Find(a => a.Text == s);
        part.MarkComplete();
        goalParts.Remove(part);
    }

    private void FinishedOne()
    {
        count--;
        if (count <= 0)
        {
            EmitSignal(SignalName.OnFinishAnimationAll);
        }
    }
}
