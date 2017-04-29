using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MenuManager : MonoBehaviour {

	private bool[] playersJoined = new bool[4];

	private bool playerSetupScreen;

	public void PlayerInput (int playerIndex, string input) {
		if (input == "Start" && !playersJoined[playerIndex]) {
			OnPlayerJoin (playerIndex);
		}
	}

	private void StartInput (int playerIndex) {
		if (!playersJoined[playerIndex]) {

		}
	}

	private void OnPlayerJoin (int playerIndex) {
		playersJoined [playerIndex] = true;
	}

	private void OnPlayerQuit (int playerIndex) {

	}
}

[System.Serializable]
public class MenuUIElements {
	public GameObject[] panels;
	public GameObject[] mainIndicators;
	public GameObject[] joinTexts;
	public GameObject[] readyTexts;
}
