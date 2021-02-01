using UnityEngine;
using System.Collections;

public class RedLaser : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerHitSound;
	public float redDamage = 50f;

	public float RedDamage() {
		return redDamage;
	}

	public void Hit() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy (gameObject);
		if (collider.tag != "Shredder") {
			Instantiate (explosion, transform.position, Quaternion.identity);
		}

		if (collider.tag == "Player") {
			Instantiate (playerHitSound, transform.position, Quaternion.identity);
		}
	}
}