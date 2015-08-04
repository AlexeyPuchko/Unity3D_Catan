using UnityEngine;
using System.Collections.Generic;

public class PlayerDeck : MonoBehaviour {
	
	private List<ResourceCard> _deckBrick = new List<ResourceCard>();
	
	private List<ResourceCard> _deckHay = new List<ResourceCard>();
	
	private List<ResourceCard> _deckStone = new List<ResourceCard>();
	
	private List<ResourceCard> _deckWood = new List<ResourceCard>();
	
	private List<ResourceCard> _deckWool = new List<ResourceCard>();
	
	public void OnGot(ResourceCard card) {
		GetDeck(card.ResourceType).Add(card);
	}
	
	private List<ResourceCard> GetDeck(ResourceType type) {
		
		List<ResourceCard> deck = null;
		switch (type) {
			case ResourceType.Brick:
			deck = _deckBrick;
			break;
			case ResourceType.Hay:
			deck = _deckHay;
			break;
			case ResourceType.Stone:
			deck = _deckStone;
			break;
			case ResourceType.Wood:
			deck = _deckWood;
			break;
			case ResourceType.Wool:
			deck = _deckWool;
			break;
		}
		return deck;
	}
	
	public Vector3 GetPosition(ResourceCard card) {
		int count = transform.childCount;
		var deck = GetDeck(card.ResourceType);
		var position = new Vector3(-0.3f*count,0,0.04f*count);
		return position;
		
	}
	
	public ResourceCard GetResourceCard(ResourceType type) {
		
		var deck = GetDeck(type);
		var card = deck[deck.Count - 1];
		return card;
	}
	
	public List<ResourceCard> GetCards(Dictionary<ResourceType,int> cardsTypes) {
		var allCards = new List<ResourceCard>();
		foreach (ResourceType type in cardsTypes.Keys) {
			int count = cardsTypes[type];
			Debug.Log(type+":"+count);
			var deck = GetDeck(type);
			Debug.Log("DECK:" +type+":"+deck.Count);
			if (deck.Count < count)
				return null;
			var cards = deck.GetRange(deck.Count - count, count);
			deck.RemoveRange(deck.Count - count, count);
			allCards.AddRange(cards);
		}
		Debug.Log(allCards.ToString());
		return allCards;
	}
	
}