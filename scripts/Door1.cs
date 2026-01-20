using Godot;
using System;

//Every door script is the same, so I will only explain it here. It checks if the player is in the door hitbox, and if so, play the door open sound and change scene

public partial class Door1 : Area2D
{
    public bool Entered = false;  

    public void _on_body_entered(Node2D body)
    {
        if (body is CharacterBody2D player)
            Entered = true;
    }

    public void _on_body_exited(Node2D body)
    {
        if (body is CharacterBody2D player)
            Entered = false;
    }

    public override void _Process(double delta)
    {
        if (Entered)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                AudioManager.Instance.OpenDoorSound();
                GetTree().ChangeSceneToFile("res://scenes/level1.tscn");
            }
        }
    }

}
