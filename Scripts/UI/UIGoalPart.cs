using Godot;
using System;

public partial class UIGoalPart : Control
{
    [Export] private Label label;
    [ExportCategory("Animation")]
    [Export] private float fadeOutTime = 0.2f;
    [Export] private float fadeOutSizeMod = 1.2f;

    [Signal]
    public delegate void OnFinishAnimationEventHandler();

    public string Text { get; private set; }

    private Interpolator interpolator;

    private Vector2 baseScale;

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator = new Interpolator());
        baseScale = Scale;
    }

    public void Init(string text)
    {
        Text = text;
        label.Text = text;
    }

    public void MarkComplete()
    {
        interpolator.Interpolate(fadeOutTime,
            new Interpolator.InterpolateObject(
                a => Scale = a,
                Scale,
                baseScale * fadeOutSizeMod,
                Easing.EaseOutSin),
            new Interpolator.InterpolateObject(
                a => Modulate = a,
                Modulate,
                new Color(Modulate, 0),
                Easing.EaseOutSin));
        interpolator.OnFinish = () => EmitSignal(SignalName.OnFinishAnimation);
    }
}
