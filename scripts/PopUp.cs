using Godot;
using System;

public partial class PopUp : Area2D
{
    public GameManager GameManager;
    [Export] public NodePath PlayerPath;
    public Player Player;
    public AudioStreamPlayer EnemyHit;

    public override void _Ready()
    {
        GameManager = GetNode<GameManager>("../../GameManager");
        EnemyHit = GetNode<AudioStreamPlayer>("EnemyHit");
        Player = GetNode<Player>(PlayerPath);
    }

    public void _on_body_entered(Node2D body)
    {
        if (body is CharacterBody2D player)
        {
            if (!Player.IsDead)
            {
                EnemyHit.Play();
                GameManager.UpdateBattery(-20);
                Player.KnockbackOnHit();
            }
        }
    }
}
