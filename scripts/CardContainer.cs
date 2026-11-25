using Godot;
using System;
using System.ComponentModel;

public partial class CardContainer : PathFollow2D
{
	// Called when the node enters the scene tree for the first time.
	
	[Export] public DraggableArea dragArea;
	[Export] private PackedScene rbCardScene;
	[Export] private Marker2D topPoint;
	public DraggableRigidBody myRBCard;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void InstantiateCardScene(Vector2 position_to_spawn)
	{
		DraggableRigidBody card_instance = rbCardScene.Instantiate<DraggableRigidBody>();
		GetTree().Root.AddChild(card_instance);
		card_instance.GlobalPosition = position_to_spawn;
		card_instance.areaToFollow = dragArea;
		card_instance.topPoint = topPoint;
		myRBCard = card_instance;
	}
	
}
