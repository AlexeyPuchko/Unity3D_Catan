using UnityEngine;
using System.Collections.Generic;

public enum CardType {
	Resource,
	Action
}
public class ResourceCard : MonoBehaviour {
	
	private static Dictionary<string, Sprite> _spritesCache = new Dictionary<string, Sprite>();
	public GameObject topSprite;	
	private ResourceType _resourceType;
	public ResourceType ResourceType{
		get {
			return _resourceType;
		}
		set {
			if (_resourceType != value) {
				_resourceType = value;
				UpdateSprite();
			}			
		}
	}
	
    public delegate void MethodContainer(ResourceCard sender);
   	public event MethodContainer OnAnimationFinished;
	
	private void UpdateSprite() {
		topSprite.GetComponent<SpriteRenderer>().sprite = GetSprite(_resourceType);
	}
	
	private static Sprite GetSprite(ResourceType type) {
		var spriteFile = "Sprites/Cards/Res/";
		switch (type) {
			case ResourceType.Brick:
			spriteFile += "brick";
			break;
			case ResourceType.Hay:
			spriteFile += "hay";
			break;
			case ResourceType.Stone:
			spriteFile += "stone";
			break;
			case ResourceType.Wood:
			spriteFile += "wood";
			break;
			case ResourceType.Wool:
			spriteFile += "wool";
			break;
		}
		if (!_spritesCache.ContainsKey(spriteFile)) {
			var sprite = Resources.Load(spriteFile, typeof(Sprite)) as Sprite;
			
			_spritesCache.Add(spriteFile, sprite);
		}
		return _spritesCache[spriteFile];
	}
	
	private void AddAnimationMoveToPlayerDeck(Vector3 endPosition) {
		var clip = new AnimationClip();
		var curve = AnimationCurve.EaseInOut(0, transform.position.x, 1, endPosition.x);
		clip.SetCurve("",typeof(Transform),"localPosition.x", curve);
		curve = AnimationCurve.EaseInOut(0, transform.position.y, 0.7f, 0f);
		clip.SetCurve("",typeof(Transform),"localPosition.y", curve);
		curve = new AnimationCurve();
		curve.AddKey(0, transform.position.z);
		var keyframe = curve[0];
		keyframe.outTangent = 1;
		curve.MoveKey(0, keyframe);
		curve.AddKey(0.4f, transform.position.z-2.5f);
		curve.AddKey(0.70f, transform.position.z-4.7f);
		curve.AddKey(1f, endPosition.z);
		keyframe = curve[3];
		keyframe.inTangent = 0;
		curve.MoveKey(3, keyframe);
		clip.SetCurve("",typeof(Transform),"localPosition.z", curve);
		clip.wrapMode = WrapMode.Once;

		var animEv= new AnimationEvent();
		animEv.functionName="animFinished";
		animEv.time=clip.length;
		clip.AddEvent(animEv);
	
		clip.legacy = true;
		GetComponent<Animation>().AddClip(clip, "MoveToPlayerDeckAnimation");
	}
	
	private void AddAnimationReturnToGameDeck(Vector3 endPosition) {
		
		var clip = new AnimationClip();
		var curve = AnimationCurve.EaseInOut(0, transform.position.x, 1, endPosition.x);
		clip.SetCurve("",typeof(Transform),"localPosition.x", curve);
		curve = AnimationCurve.EaseInOut(0, transform.position.y, 0.7f, 0f);
		clip.SetCurve("",typeof(Transform),"localPosition.y", curve);
		curve = new AnimationCurve();
		curve.AddKey(0, transform.position.z);
		var keyframe = curve[0];
		keyframe.outTangent = 1;
		curve.MoveKey(0, keyframe);
		curve.AddKey(0.4f, transform.position.z+2.5f);
		curve.AddKey(0.70f, transform.position.z+4.7f);
		curve.AddKey(1f, endPosition.z);
		keyframe = curve[3];
		keyframe.inTangent = 0;
		curve.MoveKey(3, keyframe);
		clip.SetCurve("",typeof(Transform),"localPosition.z", curve);
		clip.wrapMode = WrapMode.Once;

		//  var animEv= new AnimationEvent();
		//  animEv.functionName="animFinished";
		//  animEv.time=clip.length;
		//  clip.AddEvent(animEv);
	
		clip.legacy = true;
		GetComponent<Animation>().AddClip(clip, "ReturnToGameDeckAnimation");
	}
	
	void animFinished()	{
		OnAnimationFinished(this);
	}
	
	public void Start() {
	}
		
	public void MoveToPlayerDeck(GameObject playerDeck) {
		
		transform.parent = playerDeck.transform;
		OnAnimationFinished += new MethodContainer(playerDeck.GetComponent<PlayerDeck>().OnGot);

		var parentPos = playerDeck.transform.position;
		
		transform.position = new Vector3(transform.position.x - parentPos.x, transform.position.y - parentPos.y, transform.position.z - parentPos.z + 0.3f);
		
		AddAnimationMoveToPlayerDeck(playerDeck.GetComponent<PlayerDeck>().GetPosition(this));
		GetComponent<Animation>().Play("MoveToPlayerDeckAnimation");
	}
	public void ReturnCardToDeck(GameObject deck) {
		
		transform.parent = deck.transform;

		var parentPos = deck.transform.position;
		transform.position = new Vector3(transform.position.x - parentPos.x, transform.position.y - parentPos.y, transform.position.z - parentPos.z + 0.3f);
		
		AddAnimationReturnToGameDeck(deck.GetComponent<Deck>().GetPosition());
		GetComponent<Animation>().Play("ReturnToGameDeckAnimation");
	}
	
	public int CompareTo(ResourceCard card) {
		return this._resourceType.CompareTo(card._resourceType);
	}
}