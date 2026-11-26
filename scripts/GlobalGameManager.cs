using Godot;
using System;

public partial class GlobalGameManager : Node
{
    public static GlobalGameManager Instance { get; private set; }

    int numMana = 0;
    int maxMana = 10;
    public override void _Ready()
    {
        Instance = this;
    }
    
}
