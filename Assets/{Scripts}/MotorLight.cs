using UnityEngine;
using System.Collections;

public class MotorLight : MonoBehaviour {

	private PlayerController player;
	private Vector3 lightToPlayerDistance;

	void Start() {
		player = GameObject.FindObjectOfType<PlayerController> ();
		lightToPlayerDistance = this.transform.position - player.transform.position;
	}

	void Update(){
		this.transform.position = lightToPlayerDistance + player.transform.position;
	}
}