using Godot;
using System;

public partial class UICursor : Control
{
    public static UICursor Current { get; private set; }

    [Export] private UICardRenderer renderer;

    public Card HeldCard { get; private set; }
    public UICardHolder PreviousHolder { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseMotion mouseMotionEvent)
        {
            Position = mouseMotionEvent.Position;
        }
    }

    public void HoldCard(Card card, UICardHolder previousHolder)
    {
        if (HeldCard != null)
        {
            GD.PushError("[UICursor] : Already holding a card! Curr: " + HeldCard + ", new: " + card);
            return;
        }
        renderer.Render(HeldCard = card);
        renderer.Visible = true;
        PreviousHolder = previousHolder;
    }

    public void DropCard(Card card)
    {
        if (HeldCard != card)
        {
            GD.PushError("[UICursor] : No held card! Curr: " + HeldCard + ", new: " + card);
            return;
        }
        renderer.Visible = false;
        HeldCard = null;
    }
}
