using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	[SerializeField]
	private GameObject explosionEffect;
	[SerializeField]
	private float explosionEffectLength = 10f;


	[SerializeField]
	private float timeToDestroy = 1f; // TIme After Which laser will be destroyed if no colision happens
	private bool collided = false;
	[SerializeField] private int damage = 100;
	private float timer = 0f;

	private void OnCollisionEnter2D(Collision2D coll)
	{
		collided = true;
		if (explosionEffect != null)
		{
			GameObject explosion = Instantiate (explosionEffect, transform.position, explosionEffect.transform.rotation) as GameObject;
			Destroy (explosion, explosionEffectLength);
		}
		if (coll.gameObject.GetComponent<HealthManager> ())
			coll.gameObject.GetComponent<HealthManager> ().DecreaseHealth (damage);
		PlayerController.Instance.ReleaseMissile(gameObject);
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if ((timer >= timeToDestroy) && !collided) 
		{
			timer = 0f;
			//Destroy 
			PlayerController.Instance.ReleaseMissile(gameObject);
		}
	}
}
