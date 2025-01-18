using Godot;
using System;

public partial class UICardHolder : Control
{
    [Export] private UICardRenderer renderer;
    [ExportCategory("Animation")]
    [Export] private float hoverTime = 0.2f;
    [Export] private float hoverSizeMod = 1.2f;

    [Signal]
    public delegate void OnCardDroppedEventHandler(UICardHolder cardHolder);

    private Card card;

    private Interpolator interpolator;
    
    private Vector2 baseRendererPosition;
    private Vector2 baseRendererScale;

    public void Init(Card card)
    {
        this.card = card;
        baseRendererPosition = renderer.Position;
        baseRendererScale = renderer.Scale;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        Render();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        OnMouseExited();
        card.Unattach();
        renderer.Visible = false;
        UICursor.Current.HoldCard(card);
        card = null;
        return UICursor.Current;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor && cursor.HeldCard != null && card == null)
        {
            return true;
        }
        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor)
        {
            card = cursor.HeldCard;
            cursor.DropCard(card);
            Render();
            EmitSignal(SignalName.OnCardDropped);
            renderer.Scale = baseRendererScale * hoverSizeMod;
            OnMouseExited();
        }
        else
        {
            GD.PrintErr("[UICardHolder]: Placing non-cards!");
        }
    }

    private void OnMouseEntered()
    {
        interpolator.Interpolate(hoverTime,
            new Interpolator.InterpolateObject(
                a => renderer.Scale = a,
                renderer.Scale,
                baseRendererScale * hoverSizeMod,
                Easing.EaseInOutSin));
    }

    private void OnMouseExited()
    {
        interpolator.Interpolate(hoverTime,
            new Interpolator.InterpolateObject(
                a => renderer.Scale = a,
                renderer.Scale,
                baseRendererScale,
                Easing.EaseInOutSin));
    }

    public void Render()
    {
        if (card != null)
        {
            renderer.Render(card);
        }
        else
        {
            renderer.Visible = false;
        }
    }
}
