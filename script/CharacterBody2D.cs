using Godot;
using System;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public const float Speed = 300;
	public const float JumpVelocity = -400.0f;

	public string player_state = "";

	public string idle_direction = "";

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	//public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (direction.X == 0 && direction.Y == 0)
		{
			player_state = "idle";
		}
		else if (direction.X != 0 && direction.Y != 0)
		{
			player_state = "walk";
		}

		velocity = direction * Speed;

		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		play_animations(direction);
		MoveAndSlide();
	}

	public void play_animations(Vector2 direction)
	{

		if (direction.Y == -1)
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("back_walk");
			idle_direction = "back";
		}
		else if (direction.X == 1)
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("left_walk");
			idle_direction = "left";
		}
		else if (direction.Y == 1)
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("front_walk");
			idle_direction = "front";
		}
		else if (direction.X == -1)
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("right_walk");
			idle_direction = "right";
		}
		else
		{
			switch (idle_direction)
			{
				case "back":
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("back_idle");
					break;
				case "left":
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("left_idle");
					break;
				case "front":
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("front_idle");
					break;
				case "right":
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("right_idle");
					break;//
				default:
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("front_idle");
					break;
			}
		}
		// else if (direction.X > 0.5 && direction.Y < -0.5)
		// {
		// 	GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("front_right_walk");
		// }
		// else if (direction.X > 0.5 && direction.Y > 0.5)
		// {
		// 	GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("back_right_walk");
		// }
		// else if (direction.X < -0.5 && direction.Y < -0.5)
		// {
		// 	GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("front_left_walk");

		// }
		// else if (direction.X < -0.5 && direction.Y > 0.5)
		// {
		// 	GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("back_left_walk");
		// }
	}
}
