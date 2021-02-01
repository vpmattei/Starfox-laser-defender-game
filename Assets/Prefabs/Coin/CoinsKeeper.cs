using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinsKeeper : MonoBehaviour {

	private Text coinsKeeperText;
	private int zCoins;

	void Start () {
		coinsKeeperText = GetComponent<Text> ();
		coinsKeeperText.text = "Z Coins: 0";
	}

	public void CoinsKeeperText (int coins) {
		coinsKeeperText.text = "Z Coins: " + coins.ToString ();
	}
}
