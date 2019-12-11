using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] private GameObject projectileToSpawn; //Projectile to spawn
	[SerializeField] private float fireRate = 0.3f;
	[SerializeField] private Vector2 projectileSpawnOffset = new Vector2(0f, -1.3f);
	[SerializeField] private int scoreToIncrease = 10; //Score to be increased when this enemy is killed!
	[SerializeField] private Vector2 projectileSpeed = new Vector2(0f, -1f);
	[SerializeField] private bool moveToFro = true;
	public bool isBoss = false;
	[SerializeField] private GameObject healthBar;

	public int ScoreToIncrease{ get{ return scoreToIncrease;}}
	public enum EnemyType
	{
		Individual, Wave
	}

	public EnemyType enemyType;

	private float minHeight = 0f, maxHeight = 0f, minWidth = 0f, maxWidth = 0f;
	private Vector2 vel1, vel2;
	[SerializeField] [Range(1f, 10f)] private float smoothTime = 1f;
	private Vector2 minW, maxW;
	private float maxSpeed = 10f;
	private void Fire()
	{
		Vector2 targetPos = new Vector2 (transform.position.x + projectileSpawnOffset.x, transform.position.y + projectileSpawnOffset.y);
		GameObject proj = Instantiate (projectileToSpawn, targetPos, projectileToSpawn.transform.rotation) as GameObject;
		if (proj.GetComponent<Rigidbody2D> ())
			proj.GetComponent<Rigidbody2D> ().AddForce (projectileSpeed, ForceMode2D.Impulse);
	}

	private bool closerToMin = false, closerToMax = false, exec = false;

	private void Update()
	{
		if (enemyType == EnemyType.Individual && moveToFro) 
		{
			if (!exec) 
			{
				transform.position = Vector2.SmoothDamp (transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
				if (transform.position.x - 0.1f <= minW.x) 
				{
					closerToMin = true;
					closerToMax = false;
					exec = true;
				}	

			} else 
			{
				if (transform.position.x - 0.1f <= minW.x) 
				{
					closerToMin = true;
					closerToMax = false;
				}	
				if (transform.position.x + 0.1f >= maxW.x) 
				{
					closerToMax = true;
					closerToMin = false;
				}

				if(closerToMax)
					transform.position = Vector2.SmoothDamp (transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
				else if(closerToMin)
					transform.position = Vector2.SmoothDamp (transform.position, maxW, ref vel2, smoothTime, maxSpeed, Time.deltaTime);
			}
		}	
	}

	// Use this for initialization
	void Start () 
	{
		if (projectileToSpawn != null)
			InvokeRepeating ("Fire", 0.01f, fireRate);
		if (healthBar != null)
		{
			healthBar.SetActive (true);
		}
		if (enemyType == EnemyType.Individual) 
		{
			minHeight = Screen.height * 0.6f;
			maxHeight = Screen.height * 0.8f;
			Vector2 minH = Camera.main.ScreenToWorldPoint(new Vector2(0f, minHeight));
			Vector2 maxH = Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight));
			float height = Random.Range (minH.y, maxH.y);

			minWidth = Screen.width * 0.2f;
			maxWidth = Screen.width * 0.8f;
			minW = Camera.main.ScreenToWorldPoint(new Vector2(minWidth, 0f));
			maxW = Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f));
			minW.y = height;
			maxW.y = height;
			float width = Random.Range (minW.x, maxW.x);

			transform.position = new Vector2 (width, height);
		}
	}
}
