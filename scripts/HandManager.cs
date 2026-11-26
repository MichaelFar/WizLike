using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class HandManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private int numCardsInHand = 0;

	[Export]int maxCards = 7;
	[Export]private PackedScene[] deck;
	List<CardContainer> cardInstances = new List<CardContainer>();

	List<CardAndIndex> cardInHandIndexList = new List<CardAndIndex>();
	List<CardContainer> displacedCardInstances = new List<CardContainer>();
	List<int> cardIndexList = new List<int>();
	List<int> discardedCardIndexList = new List<int>();
	[Export]Path2D handCurve;
	
	public override void _Ready()
	{
		int card_counter = 0;
		foreach(PackedScene i in deck)
		{
			cardIndexList.Add(card_counter);
			card_counter += 1;
		}
		ShuffleDeck();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustReleased("Debug"))
		{
			DrawMultipleCards(2);
		}
		if(Input.IsActionJustReleased("DebugRepopulate"))
		{
			RepopulateDeck();
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
	public void ShuffleDeck()
	{
		//ListExtensions.Shuffle<int>(cardIndexList);
		cardIndexList = cardIndexList.OrderBy(i => Guid.NewGuid()).ToList<int>();
	}
	public void RepopulateDeck()
	{

		foreach(int i in discardedCardIndexList)
		{
			cardIndexList.Add(i);
		}
		discardedCardIndexList = new List<int>();
		ShuffleDeck();
	}
	public void DrawCard()
	{
		if(maxCards > cardInstances.Count + displacedCardInstances.Count && cardIndexList.Count > 0)
		{
			CardContainer card_instance = deck[cardIndexList[0]].Instantiate<CardContainer>();//Replace with random draw
			handCurve.AddChild(card_instance);
			GD.Print("Drawing card at index " + cardIndexList[0]);
			cardInHandIndexList.Add(new CardAndIndex(card_instance, cardIndexList[0]));
			cardIndexList.Remove(cardIndexList[0]);
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
		int z_counter = 0;
		foreach(CardContainer i in cardInstances)
		{
			current_ratio += ratio_per_card;
			i.ProgressRatio = current_ratio;
			GD.Print(i.ProgressRatio);
			GD.Print(current_ratio);
			z_counter += 1;
			i.myRBCard.ZIndex = (z_counter);
		}
	}
	public void DiscardCard(CardContainer card_to_discard)
	{
		CardAndIndex object_to_remove = new CardAndIndex();
		foreach(CardAndIndex i in cardInHandIndexList)
		{
			if(i.card == card_to_discard)
			{
				discardedCardIndexList.Add(i.index);
				object_to_remove = i;
				break;
			}
		}
		cardInHandIndexList.Remove(object_to_remove);
		
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

public struct CardAndIndex
{
	public CardAndIndex(CardContainer _card, int _index)
	{
		
		card = _card;
		index = _index;
	}
	public int index;
	public CardContainer card;
}
public static class ListExtensions
{
	public static void Shuffle<T>(this IList<T> ts) 
	{
		RandomNumberGenerator rand_obj = new RandomNumberGenerator();
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i) 
		{
			var r =  rand_obj.RandiRange(i, count);
			var tmp = ts[i];
			ts[i] = ts[r];
			ts[r] = tmp;
		}
		//GD.Print(ts);
	}
	
}
