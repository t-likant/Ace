using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController Instance;


	public GameMenuManager.PlayerMovementInputType movementInputType;
	[SerializeField]
	private KeyCode fwrd = KeyCode.UpArrow, bck = KeyCode.DownArrow, left = KeyCode.LeftArrow, right = KeyCode.RightArrow;

	[SerializeField]
	private VButton fwVB, bVB, lVB, rVB;

	[SerializeField]
	private float Speed = 10f, SpeedaccelX = 20f, SpeedaccelY = 2.5f;
	[SerializeField]
	private Vector2 minPos, maxPos;

	private Vector2 pos;


	[Header("Laser")]
	[SerializeField]
	private GameObject laser;
	[SerializeField] private AudioClip laserShootSFX;
	[SerializeField]
	private Vector2 laserSpeed = new Vector2 (0f, 1f);
	[SerializeField]
	private Vector3 spawnOffset;
	[SerializeField]
	private float laserFireRate = 0.2f;
	[SerializeField]
	private KeyCode laserKey = KeyCode.Mouse1;
	[SerializeField] private bool shootLasersAutomatically = true; // Shoot lasers automatically when player is in pointer based mode.
	private ObjectPool laserPool;
	[SerializeField]
	private int laserPoolSize = 30;


	[Header("Missile")]
	[SerializeField]
	private GameObject missile;
	[SerializeField]
	private Vector2 missileSpeed = new Vector2 (0f, 1f);
	[SerializeField] private AudioClip missileShootSFX;
	[SerializeField]
	private Vector3 spawnOffsetMissile;
	[SerializeField]
	private KeyCode missileKey = KeyCode.Mouse1;

	private ObjectPool missilePool;
	[SerializeField]
	private int missilePoolSize = 30;

	[SerializeField]
	private VButton laserVB, missileVB;
	private bool laserPressed, missilePressed;

	private float laserInterval = 0.3f;
	private float counter = 0f;
	// Use this for initialization
	void Start () {
		Instance = this;
		laserPool = new ObjectPool (laser, laserPoolSize, "PlayerLaserPool");
		missilePool = new ObjectPool (missile, missilePoolSize, "PlayerMissilePool");
	}

	public void ReleaseLaser(GameObject laser)
	{
		laserPool.ReturnInstance (laser);
	}

	public void ReleaseMissile(GameObject missile)
	{
		missilePool.ReturnInstance (missile);
	}

	private void Fire()
	{
		if (GameStatsManager.Instance.CheckifCanShootLaser (1)) 
		{
			GameObject laserInstance = laserPool.GetInstance ();
			laserInstance.transform.position = transform.position + spawnOffset;
			laserInstance.GetComponent<Rigidbody2D> ().AddForce (laserSpeed, ForceMode2D.Impulse);
			GameStatsManager.Instance.ShootLaserByAmount (1);
			if (GameSFX.Instance != null && laserShootSFX != null)
				GameSFX.Instance.PlaySFX (laserShootSFX, 0.67f);
		}
	}

	private void MissileFire()
	{
		if (GameStatsManager.Instance.CheckifCanShootMissile (1)) {
			GameObject missileInstance = missilePool.GetInstance ();
			missileInstance.transform.position = transform.position + spawnOffsetMissile;
			missileInstance.GetComponent<Rigidbody2D> ().AddForce (missileSpeed, ForceMode2D.Impulse);
			GameStatsManager.Instance.ShootMissileByAmount (1);
			if (GameSFX.Instance != null && missileShootSFX != null)
				GameSFX.Instance.PlaySFX (missileShootSFX, 0.67f);
		}
	}

	void GetInput()
	{
		if (laserVB != null)
			laserPressed = laserVB.value;
		if (missileVB != null)
			missilePressed = missileVB.value;
	}
	// Update is called once per frame
	void Update () 
	{
		Instance = this;
		GetInput ();
		if (GameMenuManager.Instance != null) 
		{
			movementInputType = GameMenuManager.Instance.CurrentPMIT;
			Destroy (GameMenuManager.Instance.gameObject);
		}
		if (movementInputType == GameMenuManager.PlayerMovementInputType.ButtonBased) {
			#if UNITY_STANDALONE || UNITY_WEBGL
			if (Input.GetKey (fwrd))
				transform.Translate (Speed * Vector2.up * Time.deltaTime);
			else if (Input.GetKey (bck))
				transform.Translate (Speed * Vector2.down * Time.deltaTime);

			if (Input.GetKey (left))
				transform.Translate (Speed * Vector2.left * Time.deltaTime);
			else if (Input.GetKey (right))
				transform.Translate (Speed * Vector2.right * Time.deltaTime);
			#endif

			if (fwVB != null && bVB != null && lVB != null && rVB != null) {
				if (fwVB.value)
					transform.Translate (Speed * Vector2.up * Time.deltaTime);
				else if (bVB.value)
					transform.Translate (Speed * Vector2.down * Time.deltaTime);

				if (lVB.value)
					transform.Translate (Speed * Vector2.left * Time.deltaTime);
				else if (rVB.value)
					transform.Translate (Speed * Vector2.right * Time.deltaTime);
			}

			#if UNITY_ANDROID || UNITY_IOS
			if (fwVB != null && bVB != null && lVB != null && rVB != null) {
				if (fwVB.value)
					transform.Translate (Speed * Vector2.up * Time.deltaTime);
				else if (bVB.value)
					transform.Translate (Speed * Vector2.down * Time.deltaTime);

				if (lVB.value)
					transform.Translate (Speed * Vector2.left * Time.deltaTime);
				else if (rVB.value)
					transform.Translate (Speed * Vector2.right * Time.deltaTime);
			}
			#endif
		} else if (movementInputType == GameMenuManager.PlayerMovementInputType.PointerBased) {
			Vector3 rawPos = Input.mousePosition;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (rawPos);
			if (Input.GetKey (KeyCode.Mouse0)) 
			{
				transform.position = Vector3.Lerp (transform.position, worldPos, Speed * Time.deltaTime);
				if (shootLasersAutomatically)
				{
					counter += Time.deltaTime;
					if (counter >= laserInterval) 
					{
						Fire ();
						counter = 0f;
					}
				}
			}
				
		} else 
		{
			//Tilt Based Player Movement
			transform.Translate(SpeedaccelX * Time.deltaTime * Input.acceleration.x, SpeedaccelY * Time.deltaTime * Input.acceleration.y, 0f);
		}


#if UNITY_STANDALONE || UNITY_WEBGL
		if (Input.GetKeyDown (laserKey))
			InvokeRepeating ("Fire", 0.001f, laserFireRate);

		if (Input.GetKeyUp (laserKey))
			CancelInvoke ("Fire");

		if(Input.GetKeyDown(missileKey))
			MissileFire();

#endif


#if UNITY_ANDROID || UNITY_IOS
		if (laserPressed && laserVB.value1)
		{
		InvokeRepeating ("Fire", 0.001f, laserFireRate);
		laserVB.value1 = false;
		}

		if(!laserPressed && !laserVB.value1)
		CancelInvoke ("Fire");

		if (missilePressed && missileVB.value1)
		{
		MissileFire ();
		missileVB.value1 = false;
		}
#endif

		pos.x = Mathf.Clamp (transform.position.x, minPos.x, maxPos.x);
		pos.y = Mathf.Clamp (transform.position.y, minPos.y, maxPos.y);

		transform.position = pos;
	}
}
