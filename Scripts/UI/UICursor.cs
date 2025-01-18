using Godot;
using System;

public partial class UICursor : Node
{
    public static UICursor Current { get; private set; }

    [Export] private UICardRenderer renderer;

    public Card HeldCard { get; private set; }

    public void HoldCard(Card card)
    {
        if (HeldCard != null)
        {
            GD.PrintErr("[UICursor] : Already holding a card! Curr: " + HeldCard + ", new: " + card);
            return;
        }
        renderer.Render(HeldCard = card);
        renderer.Visible = true;
    }

    public void DropCard(Card card)
    {
        if (HeldCard != card)
        {
            GD.PrintErr("[UICursor] : No held card! Curr: " + HeldCard + ", new: " + card);
            return;
        }
        renderer.Visible = false;
        HeldCard = null;
    }
}
