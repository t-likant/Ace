using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour {


	[SerializeField] private GameObject[] powerUps = new GameObject[2];
	[SerializeField] [Range(0f, 1f)] private float probabilityFactor = 0.6f;
	[SerializeField] private float timeToSpawn = 6f; //In Seconds

	private float timer = 0f;

	// Update is called once per frame
	private void Update () 
	{
		int n = Random.Range (0, powerUps.Length);
		timer += Time.deltaTime;
		if (timer >= timeToSpawn) 
		{
			if (powerUps [n] != null) 
			{
				if (Random.value <= probabilityFactor) 
				{
					Instantiate (powerUps [n].gameObject, transform);
				}
			}
			timer = 0f;
		}
	}
}
