using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Rewired;

public class MenuManager : MonoBehaviour {

	public GameObject[] mainIndicators;

	public GameObject mainPanel;

	private GameManager gameManager;
	private ArboristBranchBuilder arboristCursor;

	private int indicatorIndex;

	private bool[] playersJoined = new bool[4] { true, false, false, false};
	private int numberOfPlayers = 1;

	private bool readyToPlay {
		get {
			return (numberOfPlayers > 1);
		}
	}

	[Header ("Audio")]
	public AudioClip popClip;
	public AudioClip joinClip;
	public AudioClip quitClip;

	private AudioSource audioSource;

	private void Awake () {
		gameManager = FindObjectOfType<GameManager> ();
		arboristCursor = FindObjectOfType<ArboristBranchBuilder> ();
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayerInput (int playerIndex, string input) {
		if (playerIndex == 0) {
			 //MainMenu (input);
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

			audioSource.pitch = 1;
			audioSource.PlayOneShot (popClip);

		} else if (input == "Select") {
			if (indicatorIndex == 0 && readyToPlay) {
				gameManager.StartGame ();
				mainPanel.SetActive (false);
				audioSource.pitch = 1.5f;
				audioSource.PlayOneShot (popClip);
			} else if (indicatorIndex == 1) {
				ExitGame ();
			}
		}
	}

	private void PlayerSetup (int playerIndex, string input) {
		if (input == "Start" && !playersJoined [playerIndex]) {
			OnPlayerJoin (playerIndex);
		} else if (input == "Back" && playersJoined[playerIndex]) {
			OnPlayerQuit (playerIndex);
		}
	}

	private void StartGame () {
		if (readyToPlay) {
			arboristCursor.menuMode = false;
			audioSource.pitch = 1.5f;
			audioSource.PlayOneShot (popClip);
			gameManager.StartGame ();
			mainPanel.SetActive (false);
		}
	}

	private void ExitGame () {
		Application.Quit ();
	}

	private void OnPlayerJoin (int playerIndex) {
		playersJoined [playerIndex] = true;
		gameManager.SpawnMonkey (playerIndex);
		numberOfPlayers++;
		audioSource.pitch = 1;
		audioSource.PlayOneShot (joinClip);
	}

	private void OnPlayerQuit (int playerIndex) {
		if (playerIndex != 0) {
			playersJoined [playerIndex] = false;
			gameManager.OnPlayerQuit (playerIndex);
			numberOfPlayers--;
			audioSource.pitch = 1;
			audioSource.PlayOneShot (quitClip);
		}
	}
}
