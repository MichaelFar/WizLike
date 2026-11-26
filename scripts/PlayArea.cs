using Godot;
using System;

public partial class PlayArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]private Marker2D topMarker;
	private DraggableRigidBody currentCard;
	[Export]
	private Button cancelButton;
	[Signal]
	public delegate void PotentialCardInAreaEventHandler(DraggableRigidBody card);
	private bool cardEligibleToPlay = false;
	
	private bool cardInArea = false;
	public override void _Ready()
	{
		cancelButton.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustReleased("LeftClick") && cardInArea)
		{
			PutCardInZone();
		}
	}
	public void CheckCardForPlayable(Node2D node)
	{
		DraggableRigidBody potential_card = node as DraggableRigidBody;
		if(potential_card != null && currentCard == null)
		{
			currentCard = potential_card;
			cardInArea = true;
			
		}
	}
	private void PutCardInZone()
	{
		if(currentCard != null)
		{
			currentCard.areaToFollow = this;
			currentCard.topPoint = topMarker;
			
			cancelButton.Show();
			EmitSignal(SignalName.PotentialCardInArea, currentCard);	
		}
			
	}
	public void ResetArea()
	{
		currentCard = null;
		cancelButton.Hide();
	}
	public void CheckCardExit(Node2D node)
	{
		if(node as DraggableRigidBody != null)
		{
			currentCard = null;
			cardInArea = false;
		}
	}
	
}
