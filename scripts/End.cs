using Godot;
using System;

public partial class End : Node2D
{
	[Export] public Label Record;
	public bool Entered = false;

	public override void _Ready()
	{
		ShowRuntime();
	}
	
	//Shows the player speedrun time in a label called Record
	public void ShowRuntime()
	{
		int minutes = (int)(GameState.RunTime/60);
		int seconds = (int)(GameState.RunTime%60);
		int millis = (int)((GameState.RunTime*100) % 100);
		Record.Text = $"You took {minutes:00}:{seconds:00}.{millis:00}";
	}

	//Door Hitbox check
	public void _on_door_4_body_entered(Node2D body)
	{
		if (body is CharacterBody2D player)
        	Entered = true;
	}

	public void _on_door_4_body_exited(Node2D body)
	{
		if (body is CharacterBody2D player)
            Entered = false;
	}

	//If the player enters the door, reset runtime and battery level, plus brings him back to the main menu
    public override void _Process(double delta)
    {
        if (Entered)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                AudioManager.Instance.OpenDoorSound();
                GetTree().ChangeSceneToFile("res://scenes/MainMenu.tscn");
                GameState.Reset();
				GameState.ResetRunTime();
            }
        }
    }
}
