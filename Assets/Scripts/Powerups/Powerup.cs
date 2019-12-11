using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public enum PowerUpType
	{
		Ammo, Health
	}

	[SerializeField] private PowerUpType powerUpType;
	[SerializeField] private int perkToGive = 10;

	private void Start()
	{
		transform.position = new Vector2 (Random.Range (-2f, 2f), Random.Range (-2f, 0f));
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			if (powerUpType == PowerUpType.Ammo)
			{
				//Increase Ammo
				if (GameStatsManager.Instance != null)
					GameStatsManager.Instance.AddLaserByAmount (perkToGive);
			} else 
			{
				//Increase Health
				if (col.gameObject.GetComponent<HealthManager> ())
					col.gameObject.GetComponent<HealthManager> ().IncreaseHealth (perkToGive);
			}
			Destroy (gameObject);
		}
	}
}
