using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public float spawnDelay = 0.5f;

	public float speedFormation;
	public float width = 10f;
	public float height = 5f;
	public GameObject EnemyPrefab;
	public float padding;

	float xMin;
	float xMax;

	void Start () {
		SpawnUntilFull ();

		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 maxLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDistance));
		Vector3 maxRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, zDistance));
		xMin = maxLeft.x + padding;
		xMax = maxRight.x - padding;
	}

	void Update() {
		transform.position += Vector3.left * speedFormation * Time.deltaTime;
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		if (transform.position.x == xMin || transform.position.x == xMax) {
			speedFormation = -speedFormation;
		}

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull() {
		Transform freeSpace = NextFreePosition ();
		if (freeSpace) {
			GameObject enemy = Instantiate (EnemyPrefab, freeSpace.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freeSpace;
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
}
