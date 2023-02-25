using Godot;

public partial class Blob : CharacterBody2D
{
	private const int Speed = 50;
	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	[Export]
	public Vector2 Direction { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		
		velocity.X = Speed * Direction.X;

		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Velocity = velocity;

		MoveAndSlide();		
	}
}
