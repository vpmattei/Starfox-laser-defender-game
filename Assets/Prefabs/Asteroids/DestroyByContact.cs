using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject coin;
	public GameObject explosion;
	public float health;
	public int scoreValue;

	public float chance;
	private float randomValue;

	void Start() {
		randomValue = Random.value * 100;
	}

	void OnTriggerEnter2D(Collider2D collider){
		ScoreKeeper scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		GreenLaser greenLaser = collider.gameObject.GetComponent<GreenLaser> ();
		if (collider.tag == "Player") {
			Destroy (collider.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position, Quaternion.identity);
		} else if (collider.tag == "FriendlyLaser") {
			health -= greenLaser.GreenDamage ();
			greenLaser.Hit ();
			if (health <= 0) {
				scoreKeeper.Score (scoreValue);
				Destroy (collider.gameObject);
				Destroy (gameObject);
				Instantiate (explosion, transform.position, Quaternion.identity);
				if (chance >= randomValue) {
					Instantiate (coin, transform.position, Quaternion.identity);
				}
			} 
		}
	}
}
