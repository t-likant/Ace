using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsManager : MonoBehaviour {

	public static GameStatsManager Instance;

	[SerializeField] private bool infiniteLasers = false, infiniteMissiles = false;
	[SerializeField]
	private int noOfLasers = 100, noOfMissiles = 100;
	[SerializeField]
	private Text laserText, missileText;

	public bool CheckifCanShootLaser(int amount)
	{
		if (infiniteLasers)
			return true;
		bool p = false;
		if (noOfLasers - amount >= 0)
			p = true;

		return p;
	}
	public bool CheckifCanShootMissile(int amount)
	{
		if (infiniteMissiles)
			return true;
		bool p = false;
		if (noOfMissiles - amount >= 0)
			p = true;

		return p;
	}
	public void ShootLaserByAmount(int amount)
	{
		if (infiniteLasers)
			return;
		if (noOfLasers - amount >= 0)
			noOfLasers -= amount;
	}

	public void AddLaserByAmount(int amount)
	{
		if (infiniteLasers)
			return;
		if (amount >= 0)
			noOfLasers += amount;
	}

	public void ShootMissileByAmount(int amount)
	{
		if (infiniteMissiles)
			return;
		if (noOfMissiles - amount >= 0)
			noOfMissiles -= amount;
	}
	// Use this for initialization
	void Start () {
		Instance = this;
	}

	private string lasersStr = "Lasers: ", missilesStr = "Missiles: ";
	// Update is called once per frame
	void Update () {
		Instance = this;
		if(laserText!= null)
			laserText.text = (lasersStr + noOfLasers).ToString();
		if(missileText!= null)
			missileText.text = (missilesStr + noOfMissiles).ToString();
	}
}
