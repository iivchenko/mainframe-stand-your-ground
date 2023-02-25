using Godot;
public partial class EnemySpawner : Marker2D
{
	private Timer _timer;

	[Export]
	public Vector2 Direction { get; set; }

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += () =>
		{
			var scene = ResourceLoader.Load<PackedScene>("res://entities/blob.tscn");
			var blob = scene.Instantiate<Blob>();

			blob.GlobalPosition = this.GlobalPosition;
			blob.Direction = Direction;

			GetParent().AddChild(blob);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
