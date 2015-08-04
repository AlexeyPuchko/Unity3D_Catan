using UnityEngine;
using System.Collections;

public enum TilesTypes{
		Brick,
		Desert,
		Hay,
		Stone,
		Wood,
		Wool,
		Water	
	}
	
public class Tile {
	
	private TilesTypes _type;
	private int _num;
	private GameObject _go;
	
	private Resource _resource;
	
	public TilesTypes Type{
		get {
			return _type;	
		}
		set {
			_type = value;
		}
	}
	
	public GameObject GetGameObject{
		get {
			return _go;	
		}
	}
	
	public int Num {
		get {
			return _num;
		}
		set {
			_num = value;
		}
	}
	
    public delegate void MethodContainer(Tile sender);
   	public event MethodContainer OnActivate;

	
	
	public Tile(TilesTypes type, GameObject go) {
		_type = type;
		_go = go;
		_go.GetComponent<Resource>().ResourceType = ResourceTypeForTile(type);
	}
	
	private static ResourceType ResourceTypeForTile(TilesTypes tileType) {
		switch (tileType) {
			case TilesTypes.Brick:
			return ResourceType.Brick;
			case TilesTypes.Hay:
			return ResourceType.Hay;
			case TilesTypes.Stone:
			return ResourceType.Stone;
			case TilesTypes.Wood:
			return ResourceType.Wood;
			case TilesTypes.Wool:
			return ResourceType.Wool;
			default:
			return ResourceType.Brick;
		}
	}
	
	public void OnSelect(int value) {
		if (value == _num) {
			_go.GetComponent<SpriteRenderer>().material.color = Color.red;		
			OnActivate(this);	
		}
	}
}
