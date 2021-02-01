using UnityEngine;
using System.Collections;

public class SpriteRotationEnemy : MonoBehaviour {

	public Sprite[] sprites;

	int nextSprite;

	float timer = 0.1f;
	float delay = 0.1f;

	void Start(){
		nextSprite = 0;
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			LoadSprites ();
			timer = delay;
		}
	}

	void LoadSprites(){
		if (nextSprite <= 3) {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [nextSprite];
			nextSprite++;
		} else if (nextSprite >= 4) {
			nextSprite = 0;
		}
	}
}
