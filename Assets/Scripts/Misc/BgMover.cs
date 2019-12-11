using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMover : MonoBehaviour {

	[SerializeField] private float scrollSpeed = 0.3f;

	private Vector2 offset;
	// Update is called once per frame
	private void Update () 
	{
		if (!GetComponent<MeshRenderer> ())
			return;
		float scrollAmnt = Time.time * scrollSpeed;
		offset.x = 0f;
		offset.y = scrollAmnt;
		GetComponent<MeshRenderer> ().material.SetTextureOffset ("_MainTex", offset);
	}
}
