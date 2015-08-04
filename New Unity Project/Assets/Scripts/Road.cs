using UnityEngine;
using System;

public class Road : MonoBehaviour {
	
	private bool _isBuilded;
	
    public delegate void MethodContainer(Road sender);
   	public event MethodContainer OnBuild;
	   
	private Material _material;
	   
	public void Start() {
		_material = GetComponent<Renderer>().material;
	}
	
	public void Update() {
		
	}
	
	public void OnUp() {
		if (!_isBuilded) {
			_material.color = Color.black;
			_isBuilded = true;
			OnBuild(this);
		}
	}
}