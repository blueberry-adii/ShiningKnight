using Godot;
using System;

public partial class KillZone : Area2D
{
	private void _OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			if (IsInGroup("world_border"))
				player.Die();
			else if (IsInGroup("enemy"))
				_ = player.TakeDamage(1);
		}
	}
}
