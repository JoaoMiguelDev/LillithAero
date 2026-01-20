using Godot;
using System;

public partial class AeroFruit : Area2D
{

    public GameManager GameManager;

    public override void _Ready()
    {
        GameManager = GetNode<GameManager>("../../GameManager");
    }

    //Calls the level's GameManager to update the battery level and after is destroyed
    public void _on_body_entered(Node2D body)
    {
        if (body is CharacterBody2D player)
        {
            AudioManager.Instance.PickUpFruit();
            GameManager.UpdateBattery(20);
            QueueFree();
        }
           
    }

}
