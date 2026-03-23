using Godot;
using System;

public partial class ScoreLayer : CanvasLayer
{
    int score = 0;
    Label scoreLabel;

    public override void _Ready()
    {
        scoreLabel = GetNode<Label>("Control/HBoxContainer/ScoreLabel");
        UpdateUi();
    }

    public void AddPoint()
    {
        score++;
        UpdateUi();
    }

    private void UpdateUi()
    {
        scoreLabel.Text = score.ToString();
    }
}
