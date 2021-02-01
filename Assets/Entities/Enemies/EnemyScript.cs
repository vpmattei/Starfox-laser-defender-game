using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	float timer = Random.value * 6;

	public float healthInvader3 = 150f;
	public float laserSpeed;
	public int scoreValue;

	public GameObject explosion;
	public GameObject laser;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D collider){
		GreenLaser greenLaser = collider.gameObject.GetComponent<GreenLaser> ();
		if (greenLaser) {
			healthInvader3 -= greenLaser.GreenDamage ();
			greenLaser.Hit ();
			if (healthInvader3 <= 0) {
				Instantiate (explosion, transform.position, Quaternion.identity);
				Destroy (gameObject);
				scoreKeeper.Score (scoreValue);
			}
		}
	}

	void Update() {
		timer = timer - Time.deltaTime;
		if (timer <= 0) {
			GameObject projectile = Instantiate (laser, transform.position, Quaternion.identity) as GameObject;
			projectile.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -laserSpeed, 0);
			timer = Random.value*6;
			GetComponent<AudioSource> ().Play ();
		}
	}
}