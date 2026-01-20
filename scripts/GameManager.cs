using Godot;
using System;
using System.Threading.Tasks;

public partial class GameManager : Node
{
	[Export] public NodePath PlayerPath;

	[Export] public NodePath BatteryLevelPath;
	[Export] public float DeathDelay = 3.0f;
	public Player Player;
	public Label BatteryLevel;
	public Label Runtime;
	

	public override void _Ready()
	{
		Player = GetNode<Player>(PlayerPath);
		BatteryLevel = GetNode<Label>("../CanvasLayer/Control/VBoxContainer/Battery/BatteryLevel");
		BatteryLevel.Text = GameState.Battery.ToString() + "%";
		Runtime = GetNode<Label>("../CanvasLayer/Control/VBoxContainer/Clock/RunTime");
		Runtime.Text = GameState.RunTime.ToString();
	}

	//Updates the player battery level HUD
	public void UpdateBattery(int amount)
	{
		GameState.Battery = Mathf.Clamp(GameState.Battery + amount, 0, 100);

		if (GameState.Battery == 0)
			Death();

		if (BatteryLevel != null)
        	BatteryLevel.Text = GameState.Battery.ToString() + "%";	
	}

	//Every 5 seconds the player looses 5% of battery
	public void _on_battery_timer_timeout()
	{
		UpdateBattery(-5);
	}

	//Handles player death
	public async void Death()
	{
		if (!Player.IsDead)
		{
			Player.IsDead = true;
		 	Player.SfxDeath.Play();
		 	Player.Animation.Play("death");
		 	await ToSignal(GetTree().CreateTimer(DeathDelay), "timeout");
		 	GameState.Reset();
		 	GetTree().ReloadCurrentScene();
		}
	}

	//Updates the clock HUD
	public void UpdateRuntime()
	{
		int minutes = (int)(GameState.RunTime/60);
		int seconds = (int)(GameState.RunTime%60);
		int millis = (int)((GameState.RunTime*100) % 100);
		Runtime.Text = $"{minutes:00}:{seconds:00}.{millis:00}";
	}

	//GameState's run time is updated every frame
	public override void _Process(double delta)
	{
		GameState.RunTime += (float)delta;
		UpdateRuntime();
	}
}
