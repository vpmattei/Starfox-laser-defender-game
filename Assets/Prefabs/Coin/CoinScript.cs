using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	public GameObject coinEffect;

	public float speedCoin;
	public int minCoinValue;
	public int maxCoinValue;

	private int coinValue;

	void Start () {
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -speedCoin, 0);
		CoinValue ();
	}

	public int CoinValue() {
		coinValue = Random.Range(minCoinValue, maxCoinValue);
		return coinValue;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			Instantiate (coinEffect, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
