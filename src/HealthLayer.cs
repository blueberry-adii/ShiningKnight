using Godot;
using System;

public partial class HealthLayer : CanvasLayer
{
    Label HealthLabel;
    Player player;

    public override void _Ready()
    {
        HealthLabel = GetNode<Label>("Control/HBoxContainer/HealthLabel");
        player = GetTree().CurrentScene.GetNode<Player>("Player");
    }

    public override void _Process(double delta)
    {
        if (player != null)
        {
            HealthLabel.Text = player.Health.ToString();
        }
    }
}
