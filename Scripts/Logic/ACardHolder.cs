using Godot;
using System;

public abstract class ACardHolder : ICardHolder
{
    public abstract bool IsFull { get; }

    public event Action OnAttached;
    public event Action OnUnattached;

    public virtual void Attach(Card card)
    {
        if (card.AttachedHolder != null)
        {
            GD.PrintErr("[ACardHolder] : Attaching an attached card! " + card + ", " + this);
            return;
        }
        if (IsFull)
        {
            GD.PrintErr("[ACardHolder] : Attaching to a filled equation! " + card + ", " + this);
            return;
        }
        card.AttachedHolder = this;
        OnAttached?.Invoke();
    }

    public virtual void Unattach(Card card)
    {
        if (card.AttachedHolder != this)
        {
            GD.PrintErr("[ACardHolder] : Unattaching an unattached card! " + card + ", " + this);
            return;
        }
        card.AttachedHolder = null;
        OnUnattached?.Invoke();
    }
}
