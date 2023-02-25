using Godot;

public partial class Bullet : CharacterBody2D
{
	private const int Speed = 200;

	[Export]
	public Vector2 Direction { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		//Velocity = Speed * Direction;
		//MoveAndSlide();

		var collision = MoveAndCollide(Speed * Direction * (float)delta);

		if (collision != null)
		{
			QueueFree();
		}
	}
}
