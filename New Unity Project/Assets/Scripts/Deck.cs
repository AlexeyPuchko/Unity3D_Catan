using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	public CardType CardType;
	
	public ResourceType resourceType;
		
	private int _count;

	private List<GameObject> _cards = new List<GameObject>();

	private static int CardsCount(CardType type) {
		switch (type) {
			case CardType.Resource:
			return 15;
			case CardType.Action:
			return 25;
			default:
			return 0;
		}
	}
	
	private static string Prefab(CardType type) {
		switch (type) {
			case CardType.Resource:
			return "Models/ResourceCard";
			case CardType.Action:
			return "Models/ActionCard";
			default:
			return "";
		}
	}

	// Use this for initialization
	void Start () {
		InstantiateDeck();	
	}
	
	private void InstantiateDeck() {
		
		_count = CardsCount(CardType);
		for (int i = 0; i < _count; i++) {
			GameObject go = Instantiate(Resources.Load(Prefab(CardType))) as GameObject;
			go.transform.parent = transform;
			var resourceCard = go.GetComponent<ResourceCard>();
			resourceCard.ResourceType = resourceType;
			go.transform.position = new Vector3(transform.position.x, transform.position.y, -0.01f*i);
			_cards.Add(go);
		}	
	}
	
	public ResourceCard GetCard() {
		
		var card = _cards[_cards.Count-1];
		_cards.Remove(card);
		var resCard = card.GetComponent<ResourceCard>();
				
		return resCard;
	}
	
	public Vector3 GetPosition() {
		return new Vector3(0, 0, -0.01f*(transform.childCount - 1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
