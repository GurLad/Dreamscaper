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
        OnMouseExited();
        Card.Unattach();
        renderer.Visible = false;
        UICursor.Current.HoldCard(Card);
        EmitSignal(SignalName.OnCardUnattached, this);
        Card = null;
        return UICursor.Current;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor && cursor.HeldCard != null && Card == null)
        {
            return true;
        }
        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor)
        {
            Card = cursor.HeldCard;
            cursor.DropCard(Card);
            Render();
            EmitSignal(SignalName.OnCardDropped, this);
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
        if (Card != null)
        {
            renderer.Render(Card);
        }
        else
        {
            renderer.Visible = false;
        }
    }
}
