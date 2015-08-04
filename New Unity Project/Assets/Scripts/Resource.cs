using UnityEngine;
using System.Collections;

public enum ResourceType {
	Brick,
	Hay,
	Stone,
	Wood,
	Wool
}

public class Resource : MonoBehaviour {
	private ResourceType _resourceType;
	
	public ResourceType	ResourceType {
		get {
			return _resourceType;
		}
		set {
			_resourceType = value;
		}
	}
}
