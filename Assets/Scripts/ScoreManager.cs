using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[SerializeField] private Text scoreText, highScoreText;

	public static ScoreManager Instance;
	private int currentScore = 0;
	public int Score { get{ return currentScore;}}
	private string HIGHSCORE_KEY = "HighScore";


	public void IncreaseScore(int amnt)
	{
		//Increase the Score
		if (amnt > 0)
			currentScore += amnt;
	}

	public void SetHighScore()
	{
		int hS = PlayerPrefs.GetInt (HIGHSCORE_KEY);
		if (currentScore <= hS)
			return;

		PlayerPrefs.SetInt (HIGHSCORE_KEY, currentScore);
	}
	private void Awake()
	{
		Instance = this;
	}

	// Update is called once per frame
	private void Update () 
	{
		if (scoreText != null)
			scoreText.text = currentScore.ToString ();
		if (highScoreText != null)
			highScoreText.text = PlayerPrefs.GetInt (HIGHSCORE_KEY).ToString ();
	}
}
