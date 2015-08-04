using UnityEngine;
using System.Collections.Generic;

public enum TownType {
	Village,
	Town
}

public class Town : MonoBehaviour {
	
	public GameObject townModel;
	
	private List<ResourceType> _resources = new List<ResourceType>();
	
    public delegate void OnBuildContainer(Town sender);
    public delegate void OnActivateContainer(ResourceType resourceType);
   	public event OnBuildContainer OnBuild;
   	public event OnActivateContainer OnActivate;

	private bool _isBuilded;
	
	private TownType _type;
	
	public TownType Type {
		get {
			return _type;
		}
	}
	
	public List<ResourceType> Resources {
		get {
			return _resources;
		}
		set {
			_resources = value;
		}
	}
	
	void Start() {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnUp() {
		if (!_isBuilded) {
			townModel.GetComponent<Renderer>().material.color = Color.black;
			_isBuilded = true;
			OnBuild(this);
		}
	}
	
	public void OnTileActivate(Tile tile) {
		OnActivate(tile.GetGameObject.GetComponent<Resource>().ResourceType);
	}
	
}
