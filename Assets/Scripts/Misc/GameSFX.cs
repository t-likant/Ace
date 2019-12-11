using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFX : MonoBehaviour {

	public static GameSFX Instance;

	private AudioSource _adSrc;
	// Use this for initialization
	private void Awake () 
	{
		Instance = this;
		if (!GetComponent<AudioSource> ())
			gameObject.AddComponent<AudioSource> ();
		_adSrc = GetComponent<AudioSource> ();
		_adSrc.volume = 1f;
		_adSrc.loop = false;
		_adSrc.playOnAwake = false;
		_adSrc.clip = null;
	}

	public void PlaySFX(AudioClip sfx, float volume)
	{
		if(sfx != null)
			_adSrc.PlayOneShot(sfx, volume);
	}
}
