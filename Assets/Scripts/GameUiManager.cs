using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUiManager : MonoBehaviour {

	[SerializeField] private GameObject gamePlayCanvas, gameOverCanvas;

	private void Start () {
		if (gamePlayCanvas != null)
			gamePlayCanvas.SetActive (true);
		if (gameOverCanvas != null)
			gameOverCanvas.SetActive (false);
	}

	public void GameOver()
	{
		if (gamePlayCanvas != null)
			gamePlayCanvas.SetActive (false);
		if (gameOverCanvas != null)
			gameOverCanvas.SetActive (true);
	}

	public void LoadScene(int index)
	{
		SceneManager.LoadScene (index);
	}
}
