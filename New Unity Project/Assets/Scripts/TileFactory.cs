using UnityEngine;
using System.Collections.Generic;
using System;

public class TileFactory {
	
	private const String TileSpritePath = "Tiles/Sprites/";
	private const String TilePrefabFile = "Tiles/tile";
	
	private static TileFactory _sharedInstance;
	
	static public TileFactory SharedInstance(GameObject go) {
		if (_sharedInstance == null) {
			_sharedInstance = new TileFactory(go);
		}
		return _sharedInstance;
	}
	
	
	private GameObject _goTile;
	
	private Dictionary<String, Sprite> _spritesCache;
	
	
	private TileFactory (GameObject go) {
		_goTile = go;
		_spritesCache = new Dictionary<String, Sprite>();
	}
	
	private Sprite SpriteForWaterTile(WaterTilesTypes type) {
		
		Sprite sprite = null;
		String spriteName = null;
		switch (type) {
			case WaterTilesTypes.Water:
			spriteName = "water";
			break;
			case WaterTilesTypes.Dock:
			spriteName = "dock";
			break;
			case WaterTilesTypes.DockBrick:
			spriteName = "dock_brick";
			break;
			case WaterTilesTypes.DockHay:
			spriteName = "dock_hay";
			break;
			case WaterTilesTypes.DockStone:
			spriteName = "dock_stone";
			break;
			case WaterTilesTypes.DockWood:
			spriteName = "dock_wood";
			break;
			case WaterTilesTypes.DockWool:
			spriteName = "dock_wool";
			break;
		}
		if (!_spritesCache.ContainsKey(spriteName)) {
			sprite = Resources.Load(TileSpritePath + spriteName, typeof(Sprite)) as Sprite;
			_spritesCache.Add(spriteName, sprite);		
		} else {			
			 sprite = _spritesCache[spriteName];
		}	
		return sprite;
	}
	private Sprite SpriteForTile(TilesTypes type) {
		Sprite sprite = null;
		String spriteName = null;
		switch (type) {
			case TilesTypes.Brick:
			spriteName = "brick";
			break;
			case TilesTypes.Desert:
			spriteName = "desert";
			break;
			case TilesTypes.Hay:
			spriteName = "hay";
			break;
			case TilesTypes.Stone:
			spriteName = "stone";
			break;
			case TilesTypes.Wood:
			spriteName = "wood";
			break;
			case TilesTypes.Wool:
			spriteName = "wool";
			break;
		}
		if (!_spritesCache.ContainsKey(spriteName)) {
			sprite = Resources.Load(TileSpritePath + spriteName, typeof(Sprite)) as Sprite;
			_spritesCache.Add(spriteName, sprite);		
		} else {			
			 sprite = _spritesCache[spriteName];
		}	
		return sprite;
	}
	
	private int WaterTilesCount(WaterTilesTypes type) {
		int tilesCount = 0;
		switch (type) {
			case WaterTilesTypes.Water:
			tilesCount = 9;
			break;
			case WaterTilesTypes.Dock:
			tilesCount = 4;
			break;
			case WaterTilesTypes.DockBrick:
			tilesCount = 1;
			break;
			case WaterTilesTypes.DockHay:
			tilesCount = 1;
			break;
			case WaterTilesTypes.DockStone:
			tilesCount = 1;
			break;
			case WaterTilesTypes.DockWood:
			tilesCount = 1;
			break;
			case WaterTilesTypes.DockWool:
			tilesCount = 1;
			break;
		}
		return tilesCount;
	}
	
	private int TilesCount(TilesTypes type) {
		int tilesCount = 0;
		switch (type) {
			case TilesTypes.Brick:
			tilesCount = 3;
			break;
			case TilesTypes.Desert:
			tilesCount = 1;
			break;
			case TilesTypes.Hay:
			tilesCount = 4;
			break;
			case TilesTypes.Stone:
			tilesCount = 3;
			break;
			case TilesTypes.Wood:
			tilesCount = 4;
			break;
			case TilesTypes.Wool:
			tilesCount = 4;
			break;
		}		
		return tilesCount;
	}
	
	public Tile CreateTile(TilesTypes type) {
		var go = GameObject.Instantiate(_goTile);	
		go.GetComponent<SpriteRenderer>().sprite = SpriteForTile(type);	
		return new Tile(type, go);
	}
	
	public List<Tile> CreateTiles(TilesTypes type) {
		
		var tilesCount = TilesCount(type);
		var tiles = new List<Tile>();
		for (int i = 0; i < tilesCount; i++) {
			Tile tile = CreateTile(type);
			tiles.Add(tile);
		}
		return tiles;
	}
	
	public List<Tile> CreateTilesPul() {
		_goTile.SetActive(true);
		var tilesPul = new List<Tile>();
		foreach (TilesTypes type in Enum.GetValues(typeof(TilesTypes))) {
			tilesPul.AddRange(CreateTiles(type));	
		}
		_goTile.SetActive(false);				
		return tilesPul;
	}
	
	
	public Tile CreateWaterTile(WaterTilesTypes type) {
		var go = GameObject.Instantiate(_goTile);		
		go.GetComponent<SpriteRenderer>().sprite = SpriteForWaterTile(type);
		return new WaterTile(type, go);
	}
	
	public List<Tile> CreateWaterTiles(WaterTilesTypes type) {
		
		var tilesCount = WaterTilesCount(type);
		var tiles = new List<Tile>();
		for (int i = 0; i < tilesCount; i++) {
			Tile tile = CreateWaterTile(type);
			tiles.Add(tile);
		}
		return tiles;
	}
	
	public List<Tile> CreateWaterTilesPul() {
		_goTile.SetActive(true);
		var tilesPul = new List<Tile>();
		foreach (TilesTypes type in Enum.GetValues(typeof(WaterTilesTypes))) {
			tilesPul.AddRange(CreateTiles(type));	
		}
		_goTile.SetActive(false);				
		return tilesPul;
	}
	
	
}