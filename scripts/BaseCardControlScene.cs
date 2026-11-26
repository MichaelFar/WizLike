using Godot;
using System;

public partial class BaseCardControlScene : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export]private int castCost = 1;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public int GetCastCost()
	{
		return castCost;
	}
	
}
