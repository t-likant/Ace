using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour {

	public static GameMenuManager Instance;
	public enum PlayerMovementInputType
	{
		PointerBased, ButtonBased, TiltInput
	}
	private string playerMovementTypeKey = "PMIT";
	private PlayerMovementInputType _pp;
	public PlayerMovementInputType CurrentPMIT { get{ return _pp;}}
	public void QuitGame()
	{
		Application.Quit ();
	}

	public void LoadScene(int index)
	{
		SceneManager.LoadScene (index);
	}
		
	public void SwitchToTilt()
	{
		_pp = PlayerMovementInputType.TiltInput;
		PlayerPrefs.SetInt (playerMovementTypeKey, 2);
	}
	public void SwitchToPointer()
	{
		_pp = PlayerMovementInputType.PointerBased;
		PlayerPrefs.SetInt (playerMovementTypeKey, 0);
	}
	public void SwitchToButton()
	{
		_pp = PlayerMovementInputType.ButtonBased;
		PlayerPrefs.SetInt (playerMovementTypeKey, 1);
	}

	private void Start()
	{
		_pp = (PlayerMovementInputType)PlayerPrefs.GetInt (playerMovementTypeKey);
	}

	private void Awake () 
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else 
		{
			Destroy (gameObject);
		}
	}
}
