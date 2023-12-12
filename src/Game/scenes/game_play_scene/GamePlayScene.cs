using Godot;

public partial class GamePlayScene : Control
{
    public static string Path = "res://scenes/game_play_scene/game_play_scene.tscn";

    private int _lives;

    private Label _livesLbl;

    public override void _Ready()
    {
        _livesLbl = GetNode<Label>("LivesLbl");

        _lives = 10;

        _livesLbl.Text = "Lives: " + _lives;

        var player = GetNode<Player>("Player");
        player.Died += () =>
        {
            _lives--;
            _livesLbl.Text = "Lives: " + _lives;

            if (_lives <= 0)
            {
                var gameOver = (PackedScene)ResourceLoader.Load("res://scenes/game_play_scene/game_play_gameover_scene_component/game_play_gameover_scene_component.tscn");
                var inst = gameOver.Instantiate() as GamePlayGameOverSceneComponent;

                inst.Restart += () => GetTree().ReloadCurrentScene();
                inst.Exit += () => 
                {
                    // TODO: add fade
                    GetTree().ChangeSceneToFile(MainMenuScene.Path);
                };

                AddChild(inst);
            }
        };
    }

    public override void _Process(double delta)
    {
    }
}
