using Godot;
using System.Threading.Tasks;
// using SourceGeneration.Attributes;

//[Scene]
//[GameEntity]
public partial class MainMenuScene : Control
{
    public static string Path = "res://scenes/main_menu_scene/main_menu_scene.tscn";

    private Label _versionLbl;
    private Button _startBtn;
    private Button _exitBtn;

    // TODO: 
    // 1. Design version display on the main menu screen 
    public override async void _Ready()
    {
        _versionLbl = GetNode<Label>("VersionLbl");
        _startBtn = GetNode<Button>("%StartBtn");
        _exitBtn = GetNode<Button>("%ExitBtn");

        var fade = GetNode<ColorRect>("Fade");
        var time = 1.0f;

        _startBtn.Pressed += async () =>
        {
            fade.Color = new Color(0, 0, 0, 0);
            fade.Visible = true;

            var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
            tween.TweenProperty(fade, "color:a", 1.0f, time);
            //tween.Parallel().TweenProperty(this, nameof(Volume), 1.0f, time);

            await ToSignal(tween, "finished");

            fade.Visible = false;

            GetTree().ChangeSceneToFile(GamePlayScene.Path);
        };

        _exitBtn.Pressed += async () =>
        {
            fade.Color = new Color(0, 0, 0, 0);
            fade.Visible = true;

            var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
            tween.TweenProperty(fade, "color:a", 1.0f, time);
            //tween.Parallel().TweenProperty(this, nameof(Volume), 1.0f, time);

            await ToSignal(tween, "finished");

            GetTree().Quit();
        };

        // TODO: _versionLbl.Text = $"v {Version.Current}";       

        fade.Color = new Color(0, 0, 0, 1);
        fade.Visible = true;

        var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(fade, "color:a", 0.0f, time);
        //tween.Parallel().TweenProperty(this, nameof(Volume), 1.0f, time);

        await ToSignal(tween, "finished");

        fade.Visible = false;
    }

    private void _startBtn_Pressed()
    {
        throw new System.NotImplementedException();
    }
}
