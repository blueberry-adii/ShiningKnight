using Godot;
using System;

public partial class Slime : Node2D
{
	const int SPEED = 40;
	int direction = 1;

	RayCast2D RayCastLeft, RayCastRight;
	AnimatedSprite2D SlimeSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SlimeSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		RayCastLeft = GetNode<RayCast2D>("RayCastLeft");
		RayCastRight = GetNode<RayCast2D>("RayCastRight");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 position = Position;
		if (RayCastRight.IsColliding())
		{
			direction = -1;
			SlimeSprite.FlipH = true;
		}
		if (RayCastLeft.IsColliding())
		{
			direction = 1;
			SlimeSprite.FlipH = false;
		}

		position.X += (float)(direction * SPEED * delta);
		Position = position;
	}
}
