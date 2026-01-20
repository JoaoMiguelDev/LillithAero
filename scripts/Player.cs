using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	//Basic movement variables
	public float Speed = 175.0f;
	public float Aceleration = 0.1f;
	public float Deceleration = 0.1f;

	//Jump variables
	public float JumpVelocity = -300.0f;
	public bool DoubleJump = false;

	//Dash variables
	public float DashSpeed = 400.0f;
	public float DashDistance = 80.0f;
    public bool Dashing = false;
	public float DashStartingPosition = 0.0f;
	public float DashDirection = 0.0f;
	public float DashCooldown = 1.0f;
	public float DashTimer = 0.0f;
	public float CurrentDistance;

	//Death variable
	public bool IsDead = false;

	//Animation and Sfx variables
	public AnimatedSprite2D Animation;
	public AudioStreamPlayer SfxWalk;
	public AudioStreamPlayer SfxJump;
	public AudioStreamPlayer SfxDash;
	public AudioStreamPlayer SfxDeath;

    public override void _Ready()
    {
        Animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		SfxWalk = GetNode<AudioStreamPlayer>("SfxWalk");
		SfxJump = GetNode<AudioStreamPlayer>("SfxJump");
		SfxDash = GetNode<AudioStreamPlayer>("SfxDash");
		SfxDeath = GetNode<AudioStreamPlayer>("SfxDeath");
    }


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		//Handles player movement when dead
		if (IsDead)
		{
			velocity.Y += 900*(float)delta;
			velocity.X = 0;
			Velocity = velocity;
			MoveAndSlide();
			return;
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;

			if (SfxWalk != null && SfxWalk.Playing)
			{
				SfxWalk.Stop();
			}
		}

		else
		{
			DoubleJump = false;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || !DoubleJump))
		{
			velocity.Y = JumpVelocity;
			SfxJump.Play();

			if (!IsOnFloor())
			{
				DoubleJump = true;
			}
		}

		//Gets directional input
		Vector2 direction = Input.GetVector("left", "right", "ui_up", "ui_down");

		if (direction != Vector2.Zero)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, Speed * Aceleration);
			Animation.FlipH = direction.X < 0;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * Deceleration);
		}

		//Handles Player dash
		if (Input.IsActionJustPressed("dash") && direction.X != 0 && !Dashing && DashTimer <= 0)
		{
			Dashing = true;
			DashStartingPosition = Position.X;
			DashDirection = direction.X;
			DashTimer = DashCooldown;

			SfxWalk.Stop();
			SfxDash.Play();
		}

		if (Dashing)
		{
		   CurrentDistance = Mathf.Abs(Position.X - DashStartingPosition);
		   if (CurrentDistance >= DashDistance || IsOnWall())
			{
				Dashing = false;
			}

			else
			{
				velocity.Y = 0;
				velocity.X = DashDirection * DashSpeed;
			}
		}

		if(DashTimer > 0)
		{
			DashTimer -= (float)delta;
		}

		//Handles player animation
		if (!IsOnFloor())
		{
			if (velocity.Y < 0)
			{
				Animation.Play("jump");
			}
			else
			{
				Animation.Play("fall");
			}

			if (Dashing)
			{
				Animation.Play("dash");
				Animation.FlipH = DashDirection < 0;
			}
			else if(direction != Vector2.Zero)
			{
				Animation.FlipH = direction.X < 0;
			}
		}
		else
		{
			if (direction != Vector2.Zero)
            {
                Animation.Play("walk");
                Animation.FlipH = direction.X < 0;

				if (SfxWalk != null && !SfxWalk.Playing)
				{
					SfxWalk.Play();
				}
            }
            else
            {
				if (SfxWalk != null && SfxWalk.Playing)
				{
					SfxWalk.Stop();
				}
                Animation.Play("idle");
            }
		}

		Velocity = velocity;
		MoveAndSlide();
	}


	//Handle player knockback on hit
	public void KnockbackOnHit()
	{
		Vector2 velocity = Velocity;

		if (!Animation.FlipH)
		{
			velocity.Y = -250;
			velocity.X = -250;
		}
		else
		{
			velocity.Y = -250;
			velocity.X =  250;
		}
		Velocity = velocity;
	}
}
