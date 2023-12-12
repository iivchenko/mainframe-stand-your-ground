using Godot;

public partial class GamePlayGameOverSceneComponent : Control
{
    private Button _restartBtn;
    private Button _exitBtn;

    [Signal]
    public delegate void RestartEventHandler();
    [Signal]
    public delegate void ExitEventHandler();

    public override void _Ready()
    {
        _restartBtn = GetNode<Button>("%RestartBtn");
        _exitBtn = GetNode<Button>("%ExitBtn");

        _restartBtn.Pressed += () => EmitSignal(SignalName.Restart);
        _exitBtn.Pressed += () => EmitSignal(SignalName.Exit);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
