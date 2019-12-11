using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour {

	private static BGMusic instance;
	// Use this for initialization
	void Start () 
	{
		if (instance == null) 
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else 
		{
			Destroy (gameObject);
		}
	}

}
