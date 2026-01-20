using Godot;
using System;

public partial class Door3 : Area2D
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
                GetTree().ChangeSceneToFile("res://scenes/end.tscn");
                GameState.Reset();
            }
        }
    }
}
