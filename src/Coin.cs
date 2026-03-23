using Godot;
using System;

public partial class Coin : Area2D
{
	AnimationPlayer animationPlayer;
	ScoreLayer scoreLayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		scoreLayer = GetTree().CurrentScene.GetNode<CanvasLayer>("ScoreLayer") as ScoreLayer;
	}

	private void _OnBodyEntered(Node2D body)
	{
		scoreLayer.AddPoint();
		animationPlayer.Play("pickup");
	}
}
