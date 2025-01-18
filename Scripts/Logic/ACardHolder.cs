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
            GD.PushError("[ACardHolder] : Attaching an attached card! " + card + ", " + this);
            return;
        }
        if (IsFull)
        {
            GD.PushError("[ACardHolder] : Attaching to a filled equation! " + card + ", " + this);
            return;
        }
        card.AttachedHolder = this;
        OnAttached?.Invoke();
        GD.Print("[ACardHolder] : Attach " + card + " to " + this);
    }

    public virtual void Unattach(Card card)
    {
        if (card.AttachedHolder != this)
        {
            GD.PushError("[ACardHolder] : Unattaching an unattached card! " + card + ", " + this);
            return;
        }
        card.AttachedHolder = null;
        OnUnattached?.Invoke();
        GD.Print("[ACardHolder] : Unattach " + card + " to " + this);
    }
}
