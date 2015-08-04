using UnityEngine;
using System.Collections;

public class TownButton : MonoBehaviour {



	void OnMove() {	
	}
	
	void OnUp(Vector3 point) {
		
		GameObject go = Instantiate(Resources.Load("Models/town"), new Vector3(0,0,-1), Quaternion.identity) as GameObject;
	}
	
	void OnDown() {	
	}
}
