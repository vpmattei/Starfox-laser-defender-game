using UnityEngine;
using System.Collections;

public class AsteroidSpeed : MonoBehaviour {

	public float speedValue;

	void Start() {
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -speedValue);
	}
}
