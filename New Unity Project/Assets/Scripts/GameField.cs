using UnityEngine;
using System.Collections.Generic;

public class GameField : MonoBehaviour {
	
	
	//  private static GameField _instanceOf;
	
	//  public static GameField SharedInstance() {
	//  	if (_instanceOf == null) {
	//  		_instanceOf = new GameField();
	//  	}
	//  	return _instanceOf;
	//  }
		
	private List<Vector3> _townsPositions;
	private List<Vector3> _roadPositions;
	private List<Quaternion> _roadQuaternion;
	private List<Vector3> _tilesPositions = new List<Vector3>{
		new Vector3(1.5f,-1f, -0.01f),
		new Vector3(1.5f, 0, -0.01f),
		new Vector3(1.5f,1f, -0.01f),
		new Vector3(0.75f,1.5f, -0.01f),
		new Vector3(0,2f, -0.01f),
		new Vector3(-0.75f,1.5f, -0.01f),
		new Vector3(-1.5f,1f, -0.01f),
		new Vector3(-1.5f,0, -0.01f),
		new Vector3(-1.5f,-1f, -0.01f),
		new Vector3(-0.75f,-1.5f, -0.01f),
		new Vector3(0,-2f, -0.01f),
		new Vector3(0.75f,-1.5f, -0.01f),
		new Vector3(0.75f,-0.5f, -0.01f),
		new Vector3(0.75f,0.5f, -0.01f),
		new Vector3(0,1f, -0.01f),
		new Vector3(-0.75f,0.5f, -0.01f),
		new Vector3(-0.75f,-0.5f, -0.01f),
		new Vector3(0,-1f, -0.01f),
		new Vector3(0,0, -0.01f),
		};
	private List<Tile> _tiles;
	private List<GameObject> _towns;
	private List<GameObject> _availableTowns = new List<GameObject>();
	private List<GameObject> _buildedTowns = new List<GameObject>();
	private List<GameObject> _roads;
	private List<GameObject> _availableRoads = new List<GameObject>();
	private List<GameObject> _buildedRoads = new List<GameObject>();
	public GameObject player;	
	private Vector3 tileSize;
	private float tileWidht, tileHeigh;
	
	public GameObject tilesContainer;
	public GameObject townsContainer;
	public GameObject roadsContainer;
	
		
	private void InstantiateTiles () {
		
		var tile = Instantiate(Resources.Load("Tiles/tile")) as GameObject;
				
		var tileFactory = TileFactory.SharedInstance(tile);
		
		_tiles = tileFactory.CreateTilesPul();
		
		Shuffle<Tile>(_tiles);
		
		tileSize = tile.GetComponent<SpriteRenderer>().bounds.size;
		tileWidht = tileSize.x;
		tileHeigh = tileSize.y;
		
		var diceRollControl = GetComponent<DiceRollControl>();
		
		for (int q = 0; q < _tilesPositions.Count; q++) {
			var old = _tilesPositions[q];
			_tilesPositions[q] = new Vector3(old.x*tileWidht, old.y*tileHeigh, old.z);
		}
		
		int i = 0;
		
		foreach (Vector3 position in _tilesPositions) {
			var t = _tiles[i];
			t.Num = i++;
			t.GetGameObject.transform.position = new Vector3(position.x, position.y , position.z);
			diceRollControl.OnValue += new DiceRollControl.MethodContainer(t.OnSelect);
			t.GetGameObject.transform.parent = tilesContainer.transform;
		}
	}
	
	private void InstantiateTowns () {
		
		var town = Instantiate(Resources.Load("Models/town")) as GameObject;
		
		_towns = new List<GameObject>();
		
		foreach (Vector3 position in _townsPositions) {
			var newTown = Instantiate(town, position, Quaternion.identity) as GameObject;
			newTown.GetComponent<Town>().OnBuild += new Town.OnBuildContainer(BuildTown);
			_towns.Add(newTown);
			newTown.transform.parent = townsContainer.transform;
		}
		GameObject.Destroy(town, 0.0f);		
	} 
	
	private void InstantiateRoads () {
		
		var road = Instantiate(Resources.Load("Models/road")) as GameObject;
		
		_roads = new List<GameObject>();
		
		for (int i = 0; i < _roadPositions.Count; i++) {
			GameObject newRoad = Instantiate(road, _roadPositions[i], _roadQuaternion[i]) as GameObject;
			newRoad.SetActive(false);
			newRoad.GetComponent<Road>().OnBuild += new Road.MethodContainer(BuildRoad);
			_roads.Add(newRoad);
			newRoad.transform.parent = roadsContainer.transform;
		}
		GameObject.Destroy(road, 0.0f);
	} 
	
	private void CalculatePositions() {
		_townsPositions = new List<Vector3>();
		_roadPositions = new List<Vector3>();
		_roadQuaternion = new List<Quaternion>();
		foreach (Vector3 position in _tilesPositions) {
			var pos = new Vector3(position.x - tileWidht / 2,position.y,0.05f);
			if (_townsPositions.Find(t => (Mathf.Abs(t.x-pos.x) + Mathf.Abs(t.y-pos.y) < 0.001f)) == Vector3.zero)
				_townsPositions.Add(pos);
				
			var roadPos = new Vector3(pos.x + tileWidht / 8, pos.y + tileHeigh / 4, -0.05f);
			
			if (_roadPositions.Find(t => (Mathf.Abs(t.x-roadPos.x) + Mathf.Abs(t.y-roadPos.y) < 0.001f)) == Vector3.zero) {
				_roadPositions.Add(roadPos);
				_roadQuaternion.Add(Quaternion.Euler(0, 0, 60));
			}
				
			roadPos = new Vector3(pos.x + tileWidht / 8, pos.y - tileHeigh / 4, -0.05f);
			if (_roadPositions.Find(t => (Mathf.Abs(t.x-roadPos.x) + Mathf.Abs(t.y-roadPos.y) < 0.001f)) == Vector3.zero) {
				_roadPositions.Add(roadPos);
				_roadQuaternion.Add(Quaternion.Euler(0, 0, -60));
			}
				
			pos = new Vector3(position.x + tileWidht / 2,position.y,0.05f);
			if (_townsPositions.Find(t => (Mathf.Abs(t.x-pos.x) + Mathf.Abs(t.y-pos.y) < 0.001f)) == Vector3.zero)
				_townsPositions.Add(pos);
				
			pos = new Vector3(position.x - tileWidht / 4,position.y +  tileHeigh / 2,0.05f);
			if (_townsPositions.Find(t => (Mathf.Abs(t.x-pos.x) + Mathf.Abs(t.y-pos.y) < 0.001f)) == Vector3.zero)
				_townsPositions.Add(pos);
				
			pos = new Vector3(position.x + tileWidht / 4,position.y + tileHeigh / 2,0.05f);
			if (_townsPositions.Find(t => (Mathf.Abs(t.x-pos.x) + Mathf.Abs(t.y-pos.y) < 0.001f)) == Vector3.zero)
				_townsPositions.Add(pos);
				
			roadPos = new Vector3(pos.x + tileWidht / 8, pos.y - tileHeigh / 4, -0.05f);
			if (_roadPositions.Find(t => (Mathf.Abs(t.x-roadPos.x) + Mathf.Abs(t.y-roadPos.y) < 0.001f)) == Vector3.zero) {
				_roadPositions.Add(roadPos);
				_roadQuaternion.Add(Quaternion.Euler(0, 0, 120));
			}
				
			roadPos = new Vector3(pos.x - tileWidht / 4, pos.y, -0.05f);
			if (_roadPositions.Find(t => (Mathf.Abs(t.x-roadPos.x) + Mathf.Abs(t.y-roadPos.y) < 0.001f)) == Vector3.zero) {
				_roadPositions.Add(roadPos);
				_roadQuaternion.Add(Quaternion.Euler(0, 0, 0));
			}
				
				
			pos = new Vector3(position.x - tileWidht / 4,position.y - tileHeigh / 2,0.05f);
			if (_townsPositions.Find(t => (Mathf.Abs(t.x-pos.x) + Mathf.Abs(t.y-pos.y) < 0.001f)) == Vector3.zero)
				_townsPositions.Add(pos);
				
			pos = new Vector3(position.x + tileWidht / 4,position.y - tileHeigh / 2,0.05f);
			if (_townsPositions.Find(t => (Mathf.Abs(t.x-pos.x) + Mathf.Abs(t.y-pos.y) < 0.001f)) == Vector3.zero)
				_townsPositions.Add(pos);	
				
			roadPos = new Vector3(pos.x + tileWidht / 8, pos.y + tileHeigh / 4, -0.05f);
			if (_roadPositions.Find(t => (Mathf.Abs(t.x-roadPos.x) + Mathf.Abs(t.y-roadPos.y) < 0.001f)) == Vector3.zero) {
				_roadPositions.Add(roadPos);
				_roadQuaternion.Add(Quaternion.Euler(0, 0, -120));
			}
				
			roadPos = new Vector3(pos.x - tileWidht / 4, pos.y, -0.05f);
			if (_roadPositions.Find(t => (Mathf.Abs(t.x-roadPos.x) + Mathf.Abs(t.y-roadPos.y) < 0.001f)) == Vector3.zero) {
				_roadPositions.Add(roadPos);
				_roadQuaternion.Add(Quaternion.Euler(0, 0, 0));
			}		
		}
	}	
	void Start () {
		Physics.gravity = new Vector3(0,0,-Physics.gravity.y);
		
		InstantiateTiles();
		CalculatePositions();
		
		InstantiateTowns();
		InstantiateRoads();		
	}
	
	public void BuildTown(Town sender) {
		
		_buildedTowns.Add(sender.gameObject);
		var townCollider = sender.GetComponent<Collider>();
		foreach (Tile tile in _tiles) {
			var tileCollider = tile.GetGameObject.GetComponent<Collider>();
			if (tileCollider.bounds.Intersects(townCollider.bounds)) {
				sender.Resources.Add(tile.GetGameObject.GetComponent<Resource>().ResourceType);
				tile.OnActivate += new Tile.MethodContainer(sender.OnTileActivate); 
				if (sender.Resources.Count == 3) {
					break;
				}
			}
		}		
		_towns.Remove(sender.gameObject);
		if (_availableTowns.Contains(sender.gameObject)) {
			_availableTowns.Remove(sender.gameObject);
		} 
		_roads.FindAll(r => (Mathf.Pow(Mathf.Pow(r.transform.position.x - sender.transform.position.x, 2) + Mathf.Pow(r.transform.position.y - sender.transform.position.y, 2),0.5f) <= 0.7f)).ForEach(
			delegate(GameObject go){
				if (!_availableRoads.Contains(go)) {
					_availableRoads.Add(go);
				}
			}
		);
		_availableRoads.ForEach(delegate(GameObject go){go.SetActive(true);});
		var unavailableTowns = _towns.FindAll(t => (Mathf.Pow(Mathf.Pow(t.transform.position.x - sender.transform.position.x, 2) + Mathf.Pow(t.transform.position.y - sender.transform.position.y, 2),0.5f) <= 1.4f));
		unavailableTowns.ForEach(delegate(GameObject go){
			if (_availableTowns.Contains(go)) {
				_availableTowns.Remove(go);
			} 
			_towns.Remove(go);		
			GameObject.Destroy(go, 0.0f);
		});
		unavailableTowns.Clear();
		if (_buildedTowns.Count == 2) {
			_towns.ForEach(delegate(GameObject go){go.SetActive(false);});
		}
		player.GetComponent<Player>().OnTownBuild(sender);
	}
	
	public void BuildRoad(Road sender) {
		_buildedRoads.Add(sender.gameObject);
		_roads.Remove(sender.gameObject);
		_availableRoads.Remove(sender.gameObject);
		_roads.FindAll(r => (Mathf.Pow(Mathf.Pow(r.transform.position.x - sender.transform.position.x, 2) + Mathf.Pow(r.transform.position.y - sender.transform.position.y, 2),0.5f) <= 1.2f)).ForEach(
			delegate(GameObject go){
				if (!_availableRoads.Contains(go)) {
					_availableRoads.Add(go);
				}
			}
		);
		_availableRoads.ForEach(delegate(GameObject go){go.SetActive(true);});
		_towns.FindAll(t => (Mathf.Pow(Mathf.Pow(t.transform.position.x - sender.transform.position.x, 2) + Mathf.Pow(t.transform.position.y - sender.transform.position.y, 2),0.5f) <= 0.7f)).ForEach(
			delegate(GameObject go){
				if (!_availableTowns.Contains(go)) {
					_availableTowns.Add(go);
				}
			}
		);
		_availableTowns.ForEach(delegate(GameObject go){go.SetActive(true);});
	}
	
	
	public static void Shuffle<T>(List<T> arr) {
		for (int i = arr.Count - 1; i > 0; i--) {
	    	int r = UnityEngine.Random.Range(0, i);
	     	T tmp = arr[i];
 	    	arr[i] = arr[r];
	     	arr[r] = tmp;
	   	}
	}	
		
	// Update is called once per frame
	void Update () {
	}	
}
