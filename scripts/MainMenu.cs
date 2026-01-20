using Godot;
using System;

public partial class MainMenu : Node2D
{
	public void _on_play_button_pressed()
	{
		AudioManager.Instance.ButtonClicked();
		GetTree().ChangeSceneToFile("res://scenes/tutorial.tscn");
	}
	public override void _Ready()
	{
		AudioManager.Instance.GameStart();
	}

}
