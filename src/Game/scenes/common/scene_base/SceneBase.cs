using Godot;
using System;

public abstract partial class SceneBase : Node2D
{
	// TODO: 
	// 1. Design portrait mode
	// 2. Design display scaling (ui and gameplay)
	//    - Mobile devices (Android, iOs)
	//    - Desktop 
	//    - Web 
	// 3. Design transition 
	//    - Visual scene transition
	//    - Sound transition (remember web sound glitch)
	// 4. Static code generation
	//    - Introduce generated Scene Path

	public override void _Ready()
	{        
	}
	
	private void LoadScene<T>() where T: SceneBase
	{
		// Implement
	}
}
