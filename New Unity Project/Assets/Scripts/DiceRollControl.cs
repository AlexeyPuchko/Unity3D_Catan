using UnityEngine;
using UnityEngine.UI;
using System.Collections;
	
public class DiceRollControl : MonoBehaviour {
	
	
    public delegate void MethodContainer(int value);
   	public event MethodContainer OnValue;
	   
	public GameObject guiText;	
	public GameObject spawnPoint;
	public void Start() {
		
	}
	
	public void Update() {
				        // check if we have to roll dice
		if (Input.GetMouseButtonDown(Dice.MOUSE_LEFT_BUTTON)){ // && !PointInRect(GuiMousePosition(), rectModeSelect)) {
	            // left mouse button clicked so roll random colored dice 2 of each dieType
	    	Dice.Clear();
			guiText.GetComponent<Text>().text ="";
	        Dice.Roll("1d6", "d6-" + "yellow", spawnPoint.transform.position, Force());
	        Dice.Roll("1d6", "d6-" + "yellow", spawnPoint.transform.position, Force());
		}
		
		
		if  (!Dice.rolling && Dice.Count("") >0) {
			int value = Dice.Value("");
			guiText.GetComponent<Text>().text = value.ToString();
	    	Dice.Clear();
			OnValue(value);
		} 
	}
	
    private Vector3 Force() {
        Vector3 rollTarget = Vector3.zero + new Vector3(0.5f - 1 * Random.value, 2 + 3 * Random.value, 0);
        return Vector3.Lerp(spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
    }	
}