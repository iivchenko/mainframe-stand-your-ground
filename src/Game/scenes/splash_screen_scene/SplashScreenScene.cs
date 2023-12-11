using Godot;
using System.Threading.Tasks;

public partial class SplashScreenScene : MarginContainer
{
    //private float Volume
    //{
    //    get => GD.Db2Linear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
    //    set => AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), GD.Linear2Db(value));
    //}

    public override async void _Ready()
    {
        var fade = GetNode<ColorRect>("Fade");
        var meow = GetNode<AudioStreamPlayer>("CatMeow");
        var time = 1.0f;
        
        fade.Color = new Color(0, 0, 0, 1);

        var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(fade, "color:a", 0.0f, time);
        //tween.Parallel().TweenProperty(this, nameof(Volume), 1.0f, time);

        await ToSignal(tween, "finished");
        meow.Play();
        await Task.Delay(500);

        tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(fade, "color:a", 1.0f, time);
        //tween.Parallel().TweenProperty(this, nameof(Volume), 0.0f, time);

        await ToSignal(tween, "finished");

        GetTree().ChangeSceneToFile(MainMenuScene.Path);
    }
}
