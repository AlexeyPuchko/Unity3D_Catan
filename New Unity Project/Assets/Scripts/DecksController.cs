using UnityEngine;
using System.Collections.Generic;

public class DecksController : MonoBehaviour {
	
	public GameObject brickDeck;
	public GameObject hayDeck;
	public GameObject stoneDeck;
	public GameObject woodDeck;
	public GameObject woolDeck;
	
	public ResourceCard GetResourceCard(ResourceType type) {
		
		var card = GetDeck(type).GetComponent<Deck>().GetCard();
		return card;
	}
	public GameObject GetDeck(ResourceType type) {
		
		GameObject deck = null;
		
		switch (type) {
			case ResourceType.Brick:
			deck = brickDeck;
			break;
			case ResourceType.Hay:
			deck = hayDeck;
			break;
			case ResourceType.Stone:
			deck = stoneDeck;
			break;
			case ResourceType.Wood:
			deck = woodDeck;
			break;
			case ResourceType.Wool:
			deck = woolDeck;
			break;
		}
		return deck;
	}
	
}