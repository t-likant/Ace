using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	private int currentHealth;

	[SerializeField] private int minHealth = 0, maxHealth = 100;
	[SerializeField] private Image healthFill;
	[SerializeField] private Text healthText;

	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private AudioClip explosionSFX;

	public void IncreaseHealth(int amnt = 1)
	{
		if (currentHealth < maxHealth)
			currentHealth += amnt;
		currentHealth = Mathf.Clamp (currentHealth, minHealth, maxHealth);
	}
	public void DecreaseHealth(int amnt = 1)
	{
		if (currentHealth <= minHealth)
			Kill ();
		if (currentHealth > minHealth)
			currentHealth -= amnt;
		currentHealth = Mathf.Clamp (currentHealth, minHealth, maxHealth);
	}

	private void Explode()
	{
		//Used to Explode the player
		if (explosionEffect == null)
			return;
		Instantiate (explosionEffect, transform.position, explosionEffect.transform.rotation);
		if (explosionSFX != null) 
		{
			if (GameSFX.Instance != null)
				GameSFX.Instance.PlaySFX (explosionSFX, 1f);
		}
	}

	private void Kill()
	{
		Explode ();
		if (GetComponent<Enemy> ())
		{
			ScoreManager.Instance.IncreaseScore (GetComponent<Enemy> ().ScoreToIncrease);
			if (GetComponent<Enemy> ().isBoss) {
				//Level Cleared 
				//Mission Passed
				//Respect++
			}
			EnemySpawner.enemiesDefeated++;
		} else if (GetComponent<PlayerController> ())
		{
			if (ScoreManager.Instance != null)
				ScoreManager.Instance.SetHighScore ();
			GameObject.FindObjectOfType<GameUiManager> ().GameOver (); // Over The Game!
		}
		Destroy (gameObject);
	}

	private float FillAmount()
	{
		return (float)((float)currentHealth / (float)maxHealth);
	}
	private void Update()
	{
		if (healthText != null)
			healthText.text = currentHealth.ToString ();
		if (healthFill != null)
			healthFill.fillAmount = FillAmount ();
		
		if (currentHealth <= minHealth)
			Kill ();
	}
	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
	}
}
