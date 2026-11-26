using Godot;
using System;

public partial class GlobalGameData : Node
{
    public static GlobalGameData Instance { get; private set; }

    public bool isDragging = false;
    public override void _Ready()
    {
        Instance = this;
    }
    
}