using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumbler;

	void Start(){
		GetComponent<Rigidbody2D> ().angularVelocity = Random.value * tumbler;
	}
}
