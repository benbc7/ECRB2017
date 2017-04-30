using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public GameObject theArboristPrefab;
	public GameObject[] monkeyPrefabs;

	public Transform[] initialSpawnPoints;

	[Header ("Game Setup")]

	[Range (0, 11)]
	public int numberOfLives;

	private GameObject theArborist;
	private GameObject[] monkies = new GameObject[3];
	private int[] livesRemaining = new int[3];

	private CameraManager cameraManager;
	private int numberOfMonkies;

	private void Awake () {
		cameraManager = FindObjectOfType<CameraManager> ();
	}

	public void StartGame (bool player1, bool player2, bool player3, bool player4) {
		if (player1) {
			if (theArborist == null) {
				theArborist = Instantiate (theArboristPrefab, initialSpawnPoints [0].position, Quaternion.identity);
			}
		}
		if (player2) {
			if (monkies [0] == null) {
				monkies [0] = Instantiate (monkeyPrefabs [0], initialSpawnPoints [1].position, Quaternion.identity);
			}
		}
		if (player3) {
			if (monkies [1] == null) {
				monkies [1] = Instantiate (monkeyPrefabs [1], initialSpawnPoints [2].position, Quaternion.identity);
			}
		}
		if (player4) {
			if (monkies [2] == null) {
				monkies [2] = Instantiate (monkeyPrefabs [2], initialSpawnPoints [3].position, Quaternion.identity);
			}
		}
	}
}
