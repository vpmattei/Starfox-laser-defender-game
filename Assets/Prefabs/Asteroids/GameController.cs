using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] asteroids;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;
	public int waveRepetitions;

	public Text gameOverText;
	public Text restartText;

	private bool gameOver;
	private bool restart;

	void Start ()
	{
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		StartCoroutine (SpawnWaves ());
	}

	void Update() {
		if (gameOver) {
			restartText.text = "Press 'R' key to restart";
			restart = true;
		}

		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < waveRepetitions; i++)
			{
				for (int x = 0; x < hazardCount; x++)
				{
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Instantiate (asteroids[x], spawnPosition, Quaternion.identity);
					yield return new WaitForSeconds (spawnWait);
				}
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				break;
			}
		}
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
