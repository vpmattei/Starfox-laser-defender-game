using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject playerExplosion;

	public float health;
	private int zCoins;

	public float lightMaxDist;
	public float lightMinDist;
	public float lightSpeed;
	private float lightTimer = 0.0f;
	
	public float speedX;
	public float speedY;
	public float paddingX;
	public float paddingYOne;
	public float paddingYTwo;

	public Sprite greenLaser;
	public Sprite blueLaser;

	public Sprite turnLeft;
	public Sprite turnRight;
	public Sprite fowardTurnLeft;
	public Sprite fowardTurnRight;
	public Sprite foward;
	public Sprite backward;
	public Sprite noTurn;

	public GameObject motorLight;
	public GameObject laser;
	public float laserSpeed;
	public float fireRate;
	public float startingFireRate = 0.35f;

	private GameController gameController;
	private CoinsKeeper coinsKeeper;
	private Animator animator;

	float xMin;
	float xMax;
	float yMax;
	float yMin;

	void Start (){
		animator = gameObject.GetComponent<Animator>();

		coinsKeeper = GameObject.Find ("zCoins").GetComponent <CoinsKeeper>();
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();

		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 maxLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDistance));
		Vector3 maxRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, zDistance));
		xMin = maxLeft.x + paddingX;
		xMax = maxRight.x - paddingX;
		yMin = maxLeft.y + paddingYOne;
		yMax = maxRight.y - paddingYTwo;
	}

	void Update () {
		MovePlayer ();

		//Firing
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", startingFireRate, fireRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ();
		}
	}

	//Move Player
	void MovePlayer() {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speedX * Time.deltaTime;
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = turnLeft;
			Stopped ();
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.position += new Vector3 (0, speedY * Time.deltaTime);
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = fowardTurnLeft;
				Accelerate ();
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				transform.position += new Vector3 (0, -speedY * Time.deltaTime);
				Decelerate ();
			}
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speedX * Time.deltaTime;
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = turnRight;
			Stopped ();
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.position += new Vector3 (0, speedY * Time.deltaTime);
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = fowardTurnRight;
				Accelerate ();
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				transform.position += new Vector3 (0, -speedY * Time.deltaTime);
				Decelerate ();
			}
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += new Vector3 (0, speedY * Time.deltaTime);
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = foward;
			Accelerate ();
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += new Vector3 (0, -speedY * Time.deltaTime);
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = backward;
			Decelerate ();
		} else {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = noTurn;
			Stopped ();
		}
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp (transform.position.y, yMin, yMax);
		transform.position = new Vector3 (newX, newY, transform.position.z);
	}

	//Fire Method
	void Fire() {
		Vector3 offset = new Vector3 (0, 1, 0);
		GameObject projectile = Instantiate (laser, transform.position + offset, Quaternion.identity) as GameObject;
		projectile.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0,laserSpeed,0);
		GetComponent<AudioSource>().Play();
	}

	//Increase-Decrease motor light + Pulsing light
	void Accelerate() {
		lightMaxDist = 0.1f;
		lightMinDist = 0.6f;
		motorLight.GetComponent<Light>().range = Mathf.PingPong(lightTimer*lightSpeed, lightMaxDist) + lightMinDist;
		lightTimer += (Time.deltaTime);
		motorLight.GetComponent<Light> ().intensity = 1.35f;
	}

	void Decelerate() {
		lightMaxDist = 0.1f;
		lightMinDist = 0.3f;
		motorLight.GetComponent<Light>().range = Mathf.PingPong(lightTimer*lightSpeed, lightMaxDist) + lightMinDist;
		lightTimer += (Time.deltaTime);
		motorLight.GetComponent<Light> ().intensity = 1.15f;
	}

	void Stopped() {
		lightMaxDist = 0.1f;
		lightMinDist = 0.5f;
		motorLight.GetComponent<Light>().range = Mathf.PingPong(lightTimer*lightSpeed, lightMaxDist) + lightMinDist;
		lightTimer += (Time.deltaTime);
		motorLight.GetComponent<Light> ().intensity = 1.25f;
	}

	void OnTriggerEnter2D(Collider2D collider){
		RedLaser redLaser = collider.gameObject.GetComponent<RedLaser> ();
		CoinScript coin = collider.gameObject.GetComponent<CoinScript> ();
		CoinsKeeper coinsKeeper = GameObject.Find ("zCoins").GetComponent <CoinsKeeper>();
		if (redLaser) {
			health -= redLaser.RedDamage ();
			redLaser.Hit ();
			if (animator.enabled == false) {
				animator.enabled = true;
			}
			animator.Play ("PlayerHitAnim");
			if (health <= 0) {
				Instantiate (playerExplosion, transform.position, Quaternion.identity);
				Destroy (motorLight);
				Destroy (gameObject);
				gameController.GameOver ();
			}
		} else if(coin){
			zCoins += coin.CoinValue ();
			coinsKeeper.CoinsKeeperText (zCoins);
		} else {
			Instantiate (playerExplosion, transform.position, Quaternion.identity);
			Destroy (motorLight);
			Destroy (gameObject);
			gameController.GameOver ();
		}
	}
}