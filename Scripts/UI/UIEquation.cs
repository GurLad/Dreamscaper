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
    [Export] private ShaderMaterial fadeMaterial;

    private Interpolator interpolator;

    public override void _Ready()
    {
        base._Ready();
        interpolator = new Interpolator();
        AddChild(interpolator);
    }

    public void Init(Level level, Equation equation)
    {
        cardHolder1.OnCardDropped += a => equation.Attach(a.Card, true);
        cardHolder1.OnCardUnattached += a => equation.Unattach(a.Card);
        cardHolder2.OnCardDropped += a => equation.Attach(a.Card, false);
        cardHolder2.OnCardUnattached += a => equation.Unattach(a.Card);

        cardHolder1.Init(null);
        cardHolder2.Init(null);

        equation.OnCombined += (s) =>
        {
            result.Text = s;
            interpolator.Interpolate(animationDuration, 
                new Interpolator.InterpolateObject(
                    t => fadeMaterial.SetShaderParameter("percent", t),
                    (float)fadeMaterial.GetShaderParameter("percent"),
                    1,
                    Easing.EaseInOutSin
                ));
        };

        equation.OnUnattached += () =>
        {
            interpolator.Interpolate(animationDuration, 
                new Interpolator.InterpolateObject(
                    t => fadeMaterial.SetShaderParameter("percent", t),
                    (float)fadeMaterial.GetShaderParameter("percent"),
                    0,
                    Easing.EaseInOutSin
                ));
        };

        level.OnMatched += (s) =>
        {
            cardHolder1.UnattachCard();
            cardHolder2.UnattachCard();
        };
    }
}
