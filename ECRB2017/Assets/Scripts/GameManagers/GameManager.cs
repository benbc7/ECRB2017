using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject theArboristPrefab;
	public GameObject[] monkeyPrefabs;

	public Transform[] initialSpawnPoints;

	[Header ("Game Setup")]

	[Range (0, 11)]
	public int numberOfLives;

	[HideInInspector]
	public List<GameObject> monkeys;

	private GameObject theArborist;

	private int[] livesRemaining = new int[3];

	private CameraManager cameraManager;
	private TrunkManager trunkManager;
	private int numberOfMonkies;

	private void Awake () {
		cameraManager = FindObjectOfType<CameraManager> ();
		trunkManager = FindObjectOfType<TrunkManager> ();
	}

	public void SpawnMonkey (int playerIndex) {
		monkeys.Add (Instantiate (monkeyPrefabs [playerIndex - 1], initialSpawnPoints [playerIndex].position, Quaternion.identity));
		livesRemaining [playerIndex - 1] = numberOfLives;
	}

	public void RespawnPlayer (int playerNumber) {

	}

	private void PlayerLost (int playerNumber) {

	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			int playerNumber = other.GetComponent<PlayerInput> ().playerNumber;
			if (livesRemaining [playerNumber - 1] == 0) {
				PlayerLost (playerNumber);
			}
			livesRemaining [playerNumber - 1]--;
			RespawnPlayer (playerNumber - 1);
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Prop") {
			trunkManager.SendMessage ("SpawnNewTrunk");
		}
	}
}
