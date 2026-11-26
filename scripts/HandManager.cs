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
	List<CardContainer> displacedCardInstances = new List<CardContainer>();
	[Export]Path2D handCurve;
	
	public override void _Ready()
	{
		
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustReleased("Debug"))
		{
			DrawMultipleCards(2);
		}
	}
	public void DrawMultipleCards(int num_cards_to_draw)
	{
		if(num_cards_to_draw < 1)
		{
			return;
		}
		SceneTreeTimer timer = GetTree().CreateTimer(0.2f);
		timer.Timeout += () => DrawMultipleCards(num_cards_to_draw - 1);
		DrawCard();
		
	}
	public void DrawCard()
	{
		if(maxCards > cardInstances.Count + displacedCardInstances.Count)
		{
			CardContainer card_instance = deck[0].Instantiate<CardContainer>();//Replace with random draw
			handCurve.AddChild(card_instance);

			
			card_instance.dragArea.Position = Vector2.Zero;
			cardInstances.Add(card_instance);
			card_instance.dragArea.RightClicked += DiscardCard; 
			//numCardsInHand += 1;
			card_instance.InstantiateCardScene(new Vector2(1920, 0));
			
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
	public void DisplaceCardFromHand(DraggableRigidBody card_to_displace)
	{
		CardContainer container = null;
		foreach(CardContainer i in cardInstances)
		{
			if(i.myRBCard == card_to_displace)
			{
				container = i;
			}
		}
		if(container != null)
		{
			cardInstances.Remove(container);
			container.dragArea.RightClicked -= DiscardCard; 
			displacedCardInstances.Add(container);
			ReadjustCardPositions();
		}
		
	}
	public void AddDisplacedCardsBackToHand()
	{
		foreach(CardContainer i in displacedCardInstances)
		{
			i.myRBCard.topPoint = i.topPoint;
			i.myRBCard.areaToFollow = i.dragArea;
			cardInstances.Add(i);
			i.dragArea.RightClicked += DiscardCard; 
		}
		displacedCardInstances = new List<CardContainer>();
		ReadjustCardPositions();
	}
}
