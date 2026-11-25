using Godot;
using System;
using System.Collections.Generic;

public partial class HandManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private int numCardsInHand = 0;

	[Export]int maxCards = 7;
	[Export]private PackedScene[] deck;
	List<CardContainer> cardInstances = new List<CardContainer>();
	[Export]Path2D handCurve;
	
	public override void _Ready()
	{
		
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustReleased("Debug"))
		{
			DrawCard();
		}
	}
	
	public void DrawCard()
	{
		if(maxCards > cardInstances.Count)
		{
			CardContainer card_instance = deck[0].Instantiate<CardContainer>();//Replace with random draw
			handCurve.AddChild(card_instance);

			
			card_instance.dragArea.Position = Vector2.Zero;
			cardInstances.Add(card_instance);
			card_instance.dragArea.RightClicked += DiscardCard; 
			//numCardsInHand += 1;
			card_instance.InstantiateCardScene(new Vector2(0, 1080));
			
		}
		ReadjustCardPositions();
	}
	public void ReadjustCardPositions()
	{
		float ratio_per_card = 1.0f / (cardInstances.Count + 1.0f);
		float current_ratio = 0.0f;
		foreach(CardContainer i in cardInstances)
		{
			current_ratio += ratio_per_card;
			i.ProgressRatio = current_ratio;
			GD.Print(i.ProgressRatio);
			GD.Print(current_ratio);
		}
	}
	public void DiscardCard(CardContainer card_to_discard)
	{
		cardInstances.Remove(card_to_discard);
		card_to_discard.myRBCard.QueueFree();
		card_to_discard.QueueFree();
		//numCardsInHand -= 1;
		ReadjustCardPositions();
	}
}
