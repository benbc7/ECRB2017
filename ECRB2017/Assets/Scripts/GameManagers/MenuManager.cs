using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Rewired;

public class MenuManager : MonoBehaviour {

	public MenuUIElements menuUIElements;

	private PlayerManager playerManager;

	private int indicatorIndex;

	private bool[] playersJoined = new bool[4] { true, false, false, false};
	private bool[] playersReady = new bool[4] { true, true, true, true};
	private int numberOfPlayers = 1;

	private bool readyToPlay {
		get {
			return (playersReady [0] == true && playersReady [1] == true && playersReady [2] == true && playersReady [3] == true && numberOfPlayers > 1);
		}
	}

	private bool playerSetupScreen;

	private void Awake () {
		playersJoined [0] = true;
		playersReady [0] = true;
		playerManager = FindObjectOfType<PlayerManager> ();
	}

	public void PlayerInput (int playerIndex, string input) {
		if (!playerSetupScreen && playerIndex == 0) {
			MainMenu (input);
		} else if (playerSetupScreen) {
			PlayerSetupScreen (playerIndex, input);
		}
	}

	private void MainMenu (string input) {
		if (input == "Up" || input == "Down") {
			if (input == "Up") {
				indicatorIndex--;
				if (indicatorIndex < 0) {
					indicatorIndex = menuUIElements.mainIndicators.Length - 1;
				}
			} else if (input == "Down") {
				indicatorIndex++;
				if (indicatorIndex > menuUIElements.mainIndicators.Length - 1) {
					indicatorIndex = 0;
				}
			}
			for (int i = 0; i < menuUIElements.mainIndicators.Length; i++) {
				menuUIElements.mainIndicators [i].SetActive (false);
			}
			menuUIElements.mainIndicators [indicatorIndex].SetActive (true);
		} else if (input == "Select") {
			if (indicatorIndex == 0) {
				menuUIElements.panels [0].SetActive (false);
				menuUIElements.panels [1].SetActive (true);
				playerSetupScreen = true;
			} else if (indicatorIndex == 1) {
				ExitGame ();
			}
		}
	}

	private void PlayerSetupScreen (int playerIndex, string input) {
		if (input == "Start" && !playersJoined [playerIndex]) {
			OnPlayerJoin (playerIndex);
		} else if (input == "Start" && playerIndex == 0 && readyToPlay) {
			StartGame ();
		}
	}

	private void StartInput (int playerIndex) {
		if (!playersJoined [playerIndex]) {
			OnPlayerJoin (playerIndex);
		}
	}

	private void StartGame () {
		menuUIElements.panels [1].SetActive (false);
		playerManager.StartGame (playersJoined[0], playersJoined [1], playersJoined [2], playersJoined [3]);
	}

	private void ExitGame () {
		Application.Quit ();
	}

	private void OnPlayerJoin (int playerIndex) {
		playersJoined [playerIndex] = true;
		numberOfPlayers++;
		menuUIElements.joinTexts [playerIndex - 1].SetActive (false);
		menuUIElements.readyTexts [playerIndex - 1].SetActive (true);
	}

	private void OnPlayerQuit (int playerIndex) {
		if (playerIndex == 0) {
			SceneManager.LoadScene (0);
		} else {
			playersJoined [playerIndex] = false;
			numberOfPlayers--;
		}
	}
}

[System.Serializable]
public class MenuUIElements {
	public GameObject[] panels;
	public GameObject[] mainIndicators;
	public GameObject[] joinTexts;
	public GameObject[] readyTexts;
}
