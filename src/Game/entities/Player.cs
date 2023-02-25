using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150.0f;
	public const float JumpVelocity = -300.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D _sprite;

	[Export]
	public PlayerOrientation Orientation { get; set; }

	public override void _Ready()
	{
		base._Ready();

		_sprite = GetNode<AnimatedSprite2D>("Sprite");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;

			Orientation = velocity.X > 0 ? PlayerOrientation.Right : PlayerOrientation.Left;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		switch(Orientation)
		{
			case PlayerOrientation.Front:
				_sprite.Play("idle");
				break;
			case PlayerOrientation.Left when velocity.X == 0:
				_sprite.Play("idle_side");
				_sprite.FlipH = true;
				break;
			case PlayerOrientation.Left when velocity.X < 0:
				_sprite.Play("walk_side");
				_sprite.FlipH = true;
				break;
			case PlayerOrientation.Right when velocity.X == 0:
				_sprite.Play("idle_side");
				_sprite.FlipH = false;
				break;
			case PlayerOrientation.Right when velocity.X > 0:
				_sprite.Play("walk_side");
				_sprite.FlipH = false;
				break;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}

public enum PlayerOrientation
{
	Front, 
	Left,
	Right,
	Back
}
