using Godot;
using System;

public partial class Killzone : Area2D
{
    public GameManager GameManager;
    public override void _Ready()
    {
        GameManager = GetNode<GameManager>("../GameManager");
    }

    public void _on_body_entered(Node2D body)
    {
        if (body is CharacterBody2D player)
        {
            GameManager.UpdateBattery(-100);
        }
    }
}
