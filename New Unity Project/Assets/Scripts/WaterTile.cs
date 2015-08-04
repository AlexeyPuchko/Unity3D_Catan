using UnityEngine;
using System;

public enum WaterTilesTypes {
		Dock,
		DockBrick,
		DockHay,
		DockStone,
		DockWood,
		DockWool,
		Water	
}

public class WaterTile: Tile {
	public WaterTile(WaterTilesTypes type, GameObject go) : base(TilesTypes.Water, go) {
		
	}
}