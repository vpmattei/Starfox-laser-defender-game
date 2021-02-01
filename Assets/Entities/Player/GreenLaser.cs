using UnityEngine;
using System.Collections;

public class GreenLaser : MonoBehaviour {

	public GameObject laserExplosion;

	public float blueDamage = 150f;
	public float greenDamage = 100f;

	public float BlueDamage() {
		return blueDamage;
	}

	public float GreenDamage() {
		return greenDamage;
	}

	public void Hit() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag != "Shredder") {
			Instantiate (laserExplosion, transform.position, Quaternion.identity);
		} 
	}
}