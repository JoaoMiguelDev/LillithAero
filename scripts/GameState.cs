using Godot;
using System;

public partial class GameState : Node
{
	public static int Battery = 100;
	public static float RunTime = 0f;

	public static void Reset()
	{
		Battery = 100;
	}

	public static void ResetRunTime()
	{
		RunTime = 0;
	}
}
