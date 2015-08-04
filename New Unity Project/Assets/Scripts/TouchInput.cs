using UnityEngine;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public LayerMask touchInputMask;
	
	private RaycastHit hit;
	private List<GameObject> touches;
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
		
		//  var oldTouches = new GameObject[touches.Count];
		//  touches.CopyTo(oldTouches);
		//  touches.Clear()
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		
			if (Physics.Raycast(ray, out hit, touchInputMask)) {
				GameObject go = hit.transform.gameObject;
				if (Input.GetMouseButton(0)) {
					go.SendMessage ("OnMove", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButtonUp(0)) {
					go.SendMessage ("OnUp", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButtonDown(0)) {
					go.SendMessage ("OnDown", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
			
		}
	}
	
}
