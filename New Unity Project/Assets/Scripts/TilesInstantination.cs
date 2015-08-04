using UnityEngine;
using System.Collections.Generic;

public class TilesInstantination : MonoBehaviour {

	public List<Tile> pul;
	private List<Vector3> _coordinates;
	
	private Vector3[] coordinates = {
		new Vector3(3.6f,-2.08f, 0.01f),
		new Vector3(3.6f, 0, 0.01f),
		new Vector3(3.6f,2.08f, 0.01f),
		new Vector3(1.8f,3.12f, 0.01f),
		new Vector3(0,4.16f, 0.01f),
		new Vector3(-1.8f,3.12f, 0.01f),
		new Vector3(-3.6f,2.08f, 0.01f),
		new Vector3(-3.6f,0, 0.01f),
		new Vector3(-3.6f,-2.08f, 0.01f),
		new Vector3(-1.8f,-3.12f, 0.01f),
		new Vector3(0,-4.16f, 0.01f),
		new Vector3(1.8f,-3.12f, 0.01f),
		new Vector3(1.8f,-1.04f, 0.01f),
		new Vector3(1.8f,1.04f, 0.01f),
		new Vector3(0,2.08f, 0.01f),
		new Vector3(-1.8f,1.04f, 0.01f),
		new Vector3(-1.8f,-1.04f, 0.01f),
		new Vector3(0,-2.08f, 0.01f),
		new Vector3(0,0, 0.01f),
		}; 
	
	
		
	// Use this for initialization
	void Start () {
		
		var tile = Instantiate(Resources.Load("Tiles/tile")) as GameObject;
		
		var size = tile.GetComponent<SpriteRenderer>().bounds.size;
		
		var tileFactory = TileFactory.SharedInstance(tile);
		
		pul = tileFactory.CreateTilesPul();
		
		Shuffle<Tile>(pul);
		
		int i = 0;
		foreach (Vector3 position in coordinates) {
			pul[i++].GetGameObject.transform.position = position;
		}
		
		_coordinates = CalculateCoordinates(size);
	}
	
	public static void Shuffle<T>(List<T> arr) {
		for (int i = arr.Count - 1; i > 0; i--) {
	    	int r = Random.Range(0, i);
	     	T tmp = arr[i];
 	    	arr[i] = arr[r];
	     	arr[r] = tmp;
	   	}
	}
	
	
	private List<Vector3> CalculateCoordinates(Vector3 size) {
		var coordinates = new List<Vector3>();
		var width = size.x;
		var height = size.y;
		for (int i = 0; i < 5; i++) {
			
		}
		return coordinates;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
