using Godot;
using System;

public partial class UICardHolder : Control
{
    [Export] private UICardRenderer renderer;
    [ExportCategory("Animation")]
    [Export] private float hoverTime = 0.2f;
    [Export] private float hoverSizeMod = 1.2f;
    [ExportCategory("Sound")]
    [Export] private AudioStream pickUpSound;
    [Export] private AudioStream dropSound;

    [Signal]
    public delegate void OnCardDroppedEventHandler(UICardHolder cardHolder);

    [Signal]
    public delegate void OnCardUnattachedEventHandler(UICardHolder cardHolder);

    public Card Card { get; private set; }

    private Interpolator interpolator;
    
    private Vector2 baseRendererPosition;
    private Vector2 baseRendererScale;

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator = new Interpolator());
    }

    public void Init(Card card)
    {
        Card = card;
        renderer.PivotOffset = renderer.Size / 2;
        baseRendererPosition = renderer.Position;
        baseRendererScale = renderer.Scale;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        Render();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (Card == null)
        {
            GD.PushWarning("[UICardHolder] : Dragging empty card!");
            return this;
        }
        OnMouseExited();
        UICursor.Current.HoldCard(Card, this);
        EmitSignal(SignalName.OnCardUnattached, this);
        Card = null;
        Render();
        SoundController.Current.PlaySFX(pickUpSound);
        return UICursor.Current;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor && cursor.HeldCard != null)
        {
            return true;
        }
        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor)
        {
            if (Card != null)
            {
                if (cursor.PreviousHolder != null && cursor.PreviousHolder.Card == null)
                {
                    EmitSignal(SignalName.OnCardUnattached, this);
                    cursor.PreviousHolder.DropCard(Card);
                    Card = null;
                }
                else
                {
                    GD.PushError("[UICardHolder] : Drop previous holder issue!");
                    return;
                }
            }
            DropCard(cursor.HeldCard);
            cursor.DropCard(Card);
            SoundController.Current.PlaySFX(dropSound);
        }
        else
        {
            GD.PushError("[UICardHolder]: Placing non-cards!");
        }
    }

    public void DropCard(Card card)
    {
        Card = card;
        Render();
        EmitSignal(SignalName.OnCardDropped, this);
        renderer.Scale = baseRendererScale * hoverSizeMod;
        OnMouseExited();
    }

    public void UnattachCard()
    {
        Card = null;
        Render();
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
        if (Card != null)
        {
            renderer.Render(Card);
            renderer.Visible = true;
        }
        else
        {
            renderer.Visible = false;
        }
    }
}
