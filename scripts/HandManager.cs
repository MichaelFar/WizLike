using Godot;
using System;
using System.Collections.Generic;

public partial class HandManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private int numCardsInHand = 5;

	List<PathFollow2D> cardHandScenes = new List<PathFollow2D>();
	[Export]Path2D handCurve;
	public override void _Ready()
	{
		foreach(Node2D i in handCurve.GetChildren())
		{
			PathFollow2D potential_scene = i as PathFollow2D;
			if(potential_scene != null)
			{
				cardHandScenes.Add(potential_scene);
			}
		}
		foreach (PathFollow2D i in cardHandScenes)
		{
			i.ProgressRatio = cardHandScenes.Count * 0.5f;
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
