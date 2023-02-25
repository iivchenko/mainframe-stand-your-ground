using Game.entities;
using Godot;

public partial class Bullet : Area2D
{
	private const int Speed = 200;

	[Export]
	public Vector2 Direction { get; set; }

	public override void _Ready()
	{
		base._Ready();

		BodyEntered += OnCollision;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Speed * Direction * (float)delta;
	}

	private void OnCollision(Node2D body)
	{
		var damagable = body as IDamagable;

		damagable?.ApplyDamage();
		QueueFree();
	}
}
