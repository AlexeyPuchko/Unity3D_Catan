using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	private List<GameObject> _buildedTowns = new List<GameObject>(); 
	private List<GameObject> _availableTowns = new List<GameObject>();
	private List<GameObject> _buildedRoads = new List<GameObject>();
	private List<GameObject> _availableRoads = new List<GameObject>();
	
	public GameObject decksController;
	public GameObject diceRollControl;
	public GameObject playerDeck;
	private Color _color;
	public Color PlayerColor {
		get {
			return _color;
		}
		set {
			_color = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTownActivate(ResourceType type) {
		var card = decksController.GetComponent<DecksController>().GetResourceCard(type);
		card.MoveToPlayerDeck(playerDeck);
	}
	
	public void OnTownBuild(Town sender) {
		
		_buildedTowns.Add(sender.gameObject);
		sender.OnActivate += new Town.OnActivateContainer(OnTownActivate);
		var cardsTypes = new Dictionary<ResourceType, int>();
		switch (sender.Type) {
			case TownType.Village:
			cardsTypes.Add(ResourceType.Brick,1);
			cardsTypes.Add(ResourceType.Hay,1);
			cardsTypes.Add(ResourceType.Wood,1);
			cardsTypes.Add(ResourceType.Wool,1);
			break;
			case TownType.Town:
			cardsTypes.Add(ResourceType.Hay,2);
			cardsTypes.Add(ResourceType.Stone,3);
			break;
		}
		Debug.Log(cardsTypes.ToString());
		var cards = playerDeck.GetComponent<PlayerDeck>().GetCards(cardsTypes);
		Debug.Log(cards.Count);
		cards.ForEach(delegate(ResourceCard card) {
			var deck = decksController.GetComponent<DecksController>().GetDeck(card.ResourceType);
			card.ReturnCardToDeck(deck);
		});
	}
	
	
}
