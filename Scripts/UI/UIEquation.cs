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
    [ExportCategory("Sound")]
    [Export] private AudioStream correctSFX;
    [Export] private AudioStream wrongSFX;

    private Interpolator interpolator;
    private bool matched = false;

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator = new Interpolator());
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
            interpolator.OnFinish = () =>
            {
                interpolator.Delay(animationEndDelay);
                interpolator.OnFinish = () =>
                {
                    if (true)
                    {
                        cardHolder1.UnattachCard();
                        cardHolder2.UnattachCard();
                        equation.RemoveAllCards();
                    }
                    SoundController.Current.PlaySFX(matched ? correctSFX : wrongSFX);
                    matched = false;
                };
            };
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
            matched = true;
        };
    }
}
