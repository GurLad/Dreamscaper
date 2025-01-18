using Godot;
using System;

public partial class UIEquation : Node
{
    [Export] private UICardHolder cardHolder1;
    [Export] private UICardHolder cardHolder2;
    [Export] private Label result;
    [ExportCategory("Animation")]
    [Export] private float animationDuration = 0.5f;
    [Export] private float animationEndDelay = 0.5f;
    [Export] private Material fadeMaterial;

    private Interpolator interpolator;

    public override void _Ready()
    {
        base._Ready();
        interpolator = new Interpolator();
        AddChild(interpolator);
    }

    public void Init(Equation equation)
    {
        cardHolder1.OnCardDropped += a => equation.Attach(a.Card, true);
        cardHolder1.OnCardUnattached += a => equation.Unattach(a.Card);
        cardHolder2.OnCardDropped += a => equation.Attach(a.Card, false);
        cardHolder2.OnCardUnattached += a => equation.Unattach(a.Card);

        equation.OnCombined += (s) =>
        {
            result.Text = s;
            interpolator.Interpolate(animationDuration, 
                new Interpolator.InterpolateObject(
                    t => fadeMaterial.Set("percent", t),
                    (float)fadeMaterial.Get("percent"),
                    1,
                    Easing.EaseInOutSin
                ));
        };

        equation.OnUnattached += () =>
        {
            interpolator.Interpolate(animationDuration, 
                new Interpolator.InterpolateObject(
                    t => fadeMaterial.Set("percent", t),
                    (float)fadeMaterial.Get("percent"),
                    1,
                    Easing.EaseInOutSin
                ));
        };
    }
}
