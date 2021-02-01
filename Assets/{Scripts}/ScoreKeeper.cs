using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public int score;

	private Text myText;

	private float timer = 10;
	private float resetTimer = 10;

	void Start() {
		myText = GetComponent<Text> ();
		Reset ();
	}

	void Update() {
		timer = timer - Time.deltaTime;
		if (timer <= 0) {
			score -= 50;
			myText.text = score.ToString ();
			timer = resetTimer;
		} if (score < 0) {
			score = 0;
			myText.text = score.ToString ();
		}
	}

	public void Score(int points) {
		timer += 1.3f;
		score += points;
		myText.text = score.ToString ();
	}

	void Reset() {
		score = 0;
		myText.text = score.ToString ();
	}
}