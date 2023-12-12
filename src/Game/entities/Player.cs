using Game.entities;
using Godot;

public partial class Player : CharacterBody2D, IDamagable
{
    public const float Speed = 150.0f;
    public const float JumpVelocity = -300.0f;

    [Signal]
    public delegate void DiedEventHandler();


    // Get the gravity from the project settings to be synced with RigidBody nodes.
    private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Vector2 _initialPosition;

    private AnimatedSprite2D _sprite;
    private Marker2D _left;
    private Marker2D _right;

    [Export]
    public PlayerOrientation Orientation { get; set; }

    public override void _Ready()
    {
        base._Ready();

        _initialPosition = this.Position;

        _sprite = GetNode<AnimatedSprite2D>("Sprite");
        _left = GetNode<Marker2D>("LeftFirePoint");
        _right = GetNode<Marker2D>("RightFirePoint");
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

        if (Input.IsActionJustPressed("player_fire"))
        {
            Fire();
        }

        Velocity = velocity;
        MoveAndSlide();

        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {

            var collision = GetSlideCollision(i).GetCollider();
            collision.GetInstanceId();

            if (collision is Blob)
            {
                Position = _initialPosition;
                EmitSignal(SignalName.Died);
            }
        }
    }

    private void Fire()
    {
        switch (Orientation)
        {
            case PlayerOrientation.Left:
                SpawnBullet(_left.GlobalPosition, Vector2.Left);
                break;         
            case PlayerOrientation.Right:
                SpawnBullet(_right.GlobalPosition, Vector2.Right);
                break;
        }
    }

    private void SpawnBullet(Vector2 position, Vector2 direction)
    {
        var scene = (PackedScene)ResourceLoader.Load("res://entities/bullet.tscn");
        var bullet = scene.Instantiate<Bullet>();

        bullet.GlobalPosition =	position;
        bullet.Direction = direction;

        GetParent().AddChild(bullet);
    }

    public void ApplyDamage()
    {
        Position = _initialPosition;
    }
}

public enum PlayerOrientation
{
    Front, 
    Left,
    Right,
    Back
}
