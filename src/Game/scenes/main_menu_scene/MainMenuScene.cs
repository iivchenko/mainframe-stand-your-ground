using Godot;
// using SourceGeneration.Attributes;

//[Scene]
//[GameEntity]
public partial class MainMenuScene : Control
{
    private Label _versionLbl;

    // TODO: 
    // 1. Design version display on the main menu screen 
    public override void _Ready()
    {
        _versionLbl = GetNode<Label>("VersionLbl");

        // TODO: _versionLbl.Text = $"v {Version.Current}";
    }
}
