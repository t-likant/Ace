using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	[SerializeField] private float timeToDestroy = 1f; // TIme After Which laser will be destroyed if no colision happens
	[SerializeField] private int damage = 25;
	private bool collided = false;

	private float timer = 0f;
	private void OnCollisionEnter2D(Collision2D coll)
	{
		collided = true;
		if (coll.gameObject.GetComponent<HealthManager> ())
			coll.gameObject.GetComponent<HealthManager> ().DecreaseHealth (damage);
		PlayerController.Instance.ReleaseLaser(gameObject);
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if ((timer >= timeToDestroy) && !collided) 
		{
			timer = 0f;
			//Destroy 
			PlayerController.Instance.ReleaseLaser(gameObject);
		}
	}
}
