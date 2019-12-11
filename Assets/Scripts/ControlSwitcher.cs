using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitcher : MonoBehaviour {

	public void SwitchControls()
	{
		if (PlayerController.Instance == null)
			return;
		if (PlayerController.Instance.movementInputType == GameMenuManager.PlayerMovementInputType.ButtonBased)
			PlayerController.Instance.movementInputType = GameMenuManager.PlayerMovementInputType.PointerBased;
		else if (PlayerController.Instance.movementInputType == GameMenuManager.PlayerMovementInputType.PointerBased)
			PlayerController.Instance.movementInputType = GameMenuManager.PlayerMovementInputType.TiltInput;
		else
			PlayerController.Instance.movementInputType = GameMenuManager.PlayerMovementInputType.ButtonBased;
	}
}
