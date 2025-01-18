using Godot;
using System;

public partial class UIBackdrop : Control
{
    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 22) //NOTIFICATION_DRAG_END)
        {
            if (!GetViewport().GuiIsDragSuccessful())
            {
                UICursor cursor = UICursor.Current;
                cursor.PreviousHolder.DropCard(cursor.HeldCard);
                cursor.DropCard(cursor.HeldCard);
            }
        }
    }
}
