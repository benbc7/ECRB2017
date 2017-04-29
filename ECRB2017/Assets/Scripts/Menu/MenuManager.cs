using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MenuManager : MonoBehaviour {

	public MenuUIElements menuUIElements;

	private bool[] playersJoined = new bool[4] { true, false, false, false};
	private bool[] playersReady = new bool[4] { true, true, true, true};
	private int numberOfPlayers;

	private bool readyToPlay {
		get {
			return (playersReady [0] == true && playersReady [1] == true && playersReady [2] == true && playersReady [3] == true && numberOfPlayers > 1);
		}
	}

	private bool playerSetupScreen;

	private void Awake () {
		playersJoined [0] = true;
		playersReady [0] = true;
	}

	public void PlayerInput (int playerIndex, string input) {
		if (input == "Start" && !playersJoined[playerIndex]) {
			OnPlayerJoin (playerIndex);
		} else if (input == "Start" && playerIndex == 0 && readyToPlay) {
			StartGame ();
		}
	}

	private void StartInput (int playerIndex) {
		if (!playersJoined[playerIndex]) {

		}
	}

	private void StartGame () {

	}

	private void OnPlayerJoin (int playerIndex) {
		playersJoined [playerIndex] = true;
		numberOfPlayers++;
		menuUIElements.joinTexts [playerIndex - 1].SetActive (false);
		menuUIElements.readyTexts [playerIndex - 1].SetActive (true);
	}

	private void OnPlayerQuit (int playerIndex) {
		playersJoined [playerIndex] = false;
		numberOfPlayers--;
	}
}

[System.Serializable]
public class MenuUIElements {
	public GameObject[] panels;
	public GameObject[] mainIndicators;
	public GameObject[] joinTexts;
	public GameObject[] readyTexts;
}
