using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private int maxEnemies = 3; // THe maximum number of enemies at a time, We are going to count enemy wave as a single Enemy
	[SerializeField] private GameObject[] enemiesToSpawn = new GameObject[2];

	[SerializeField] private int enemyDeathsToSpawnBoss = 4; // The Number of enemies we have to defeat to get to the enemy boss.
	[SerializeField] private GameObject enemyBossToSpawn;
	[SerializeField] private GameObject healthBarOfEnemyBoss;
	public static int enemiesDefeated = 0;
	private int interval = 20;

	private int GetNumberOfEnemies()
	{
		int no = 0;
		Enemy[] temp = GameObject.FindObjectsOfType<Enemy> ();
		for (int i = 0; i < temp.Length; ++i) 
		{
			if (temp [i].enemyType == Enemy.EnemyType.Individual)
				no++;
		}
		no += GameObject.FindObjectsOfType<EnemyWave> ().Length;
		return no;
	}

	private void SpawnEnemies()
	{
		if (GetNumberOfEnemies () >= maxEnemies)
			return;
		int n = Random.Range (0, enemiesToSpawn.Length);
		int i = GetNumberOfEnemies ();
		for (; i <= maxEnemies; ++i) 
		{
			n = Random.Range (0, enemiesToSpawn.Length);
			if(enemiesToSpawn[n] != null)
				Instantiate (enemiesToSpawn [n]);
		}
	}


	// Use this for initialization
	private void Start () 
	{
		SpawnEnemies ();
		if (healthBarOfEnemyBoss != null)
			healthBarOfEnemyBoss.SetActive (false);
	}

	private void SpawnEnemyBoss()
	{
		if (enemyBossToSpawn != null)
			Instantiate (enemyBossToSpawn);
	}
	private bool exec = false;
	// Update is called once per frame
	private void Update () 
	{
		if (enemiesDefeated >= enemyDeathsToSpawnBoss) 
		{
			if (!exec) 
			{
				//Spawn Enemy Boss
				SpawnEnemyBoss();
				exec = true;
			}
		} else
		{
			if(Time.frameCount % interval == 0)
				SpawnEnemies ();
		}
	}
}
