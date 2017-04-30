using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Rewired;

public class MenuManager : MonoBehaviour {

	public GameObject[] mainIndicators;

	private GameManager gameManager;

	private int indicatorIndex;

	private bool[] playersJoined = new bool[4] { true, false, false, false};
	private bool[] playersReady = new bool[4] { true, true, true, true};
	private int numberOfPlayers = 1;

	private bool readyToPlay {
		get {
			return (playersReady [0] == true && playersReady [1] == true && playersReady [2] == true && playersReady [3] == true && numberOfPlayers > 1);
		}
	}

	private void Awake () {
		playersJoined [0] = true;
		playersReady [0] = true;
		gameManager = FindObjectOfType<GameManager> ();
	}

	public void PlayerInput (int playerIndex, string input) {
		if (playerIndex == 0) {
			MainMenu (input);
		} else {
			PlayerSetup (playerIndex, input);
		}
	}

	private void MainMenu (string input) {
		if (input == "Up" || input == "Down") {
			if (input == "Up") {
				indicatorIndex--;
				if (indicatorIndex < 0) {
					indicatorIndex = mainIndicators.Length - 1;
				}
			} else if (input == "Down") {
				indicatorIndex++;
				if (indicatorIndex > mainIndicators.Length - 1) {
					indicatorIndex = 0;
				}
			}
			for (int i = 0; i < mainIndicators.Length; i++) {
				mainIndicators [i].SetActive (false);
			}
			mainIndicators [indicatorIndex].SetActive (true);
		} else if (input == "Select") {
			if (indicatorIndex == 0 && readyToPlay) {
				StartGame ();
			} else if (indicatorIndex == 1) {
				ExitGame ();
			}
		}
	}

	private void PlayerSetup (int playerIndex, string input) {
		if (input == "Start" && !playersJoined [playerIndex]) {
			OnPlayerJoin (playerIndex);
		}
	}

	private void StartInput (int playerIndex) {
		if (!playersJoined [playerIndex]) {
			OnPlayerJoin (playerIndex);
		}
	}

	private void StartGame () {

	}

	private void ExitGame () {
		Application.Quit ();
	}

	private void OnPlayerJoin (int playerIndex) {
		playersJoined [playerIndex] = true;
		numberOfPlayers++;
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
