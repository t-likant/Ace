using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour 
{
	[SerializeField] protected int damage = 10;
	[SerializeField] protected float timeToDestroy = 1f; // TIme After Which Projectile will be destroyed if no colision happens
	[SerializeField] protected bool destroyProjectile = true; 
	private bool collided = false;

	private float timer = 0f;
	private void OnCollisionEnter2D(Collision2D coll)
	{
		collided = true;
		if (coll.gameObject.GetComponent<HealthManager> ())
			coll.gameObject.GetComponent<HealthManager> ().DecreaseHealth (damage);
		Destroy (gameObject);
	}

	// Update is called once per frame
	private void Update () 
	{
		if (!destroyProjectile)
			return;
		timer += Time.deltaTime;
		if ((timer >= timeToDestroy) && !collided) 
		{
			timer = 0f;
			//Destroy 
			Destroy(gameObject);
		}
	}
}
