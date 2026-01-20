using Godot;
using System;

//Handles audio events

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    [Export] public AudioStreamPlayer SfxDoor;
    [Export] public AudioStreamPlayer SfxFruit;
    [Export] public AudioStreamPlayer SfxGameStart;
    [Export] public AudioStreamPlayer SfxButtonClicked;

    public override void _Ready()
    {
        Instance = this;
    }

    public void OpenDoorSound()
    {
        SfxDoor.Play();
    }

    public void PickUpFruit()
    {
        SfxFruit.Play();
    }

    public void GameStart()
    {
        SfxGameStart.Play();
    }

    public void ButtonClicked()
    {
        SfxButtonClicked.Play();
    }
}

