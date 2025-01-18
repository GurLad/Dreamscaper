using Godot;
using System;

public partial class UICardRenderer : Control
{
    [Export] private Label name;
    [Export] private Label emoji;

    public void Render(Card card)
    {
        name.Text = card.Name;
        emoji.Text = card.Emoji;
    }
}
