using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] monkeyPrefabs;
	public GameObject spawnPlatform;

	public Transform[] initialSpawnPoints;
	public Transform[] respawnPoints;
	private int nextPointIndex;

	public GameObject[] monkeyUIPanels;
	public Text[] livesTexts;
	public GameObject[] gameOver;

	[Header ("Game Setup")]

	public bool activeOnStart;

	[Range (0, 11)]
	public int numberOfLives;

	[HideInInspector]
	public List<GameObject> monkeys = new List<GameObject> ();

	private GameObject theArborist;

	private int[] livesRemaining = new int[3];

	private GameObject cam;
	private TrunkManager trunkManager;
	private int numberOfMonkies;

	private void Awake () {
		trunkManager = FindObjectOfType<TrunkManager> ();
		cam = GameObject.Find ("Main Camera");

		if (activeOnStart) {
			cam.SendMessage ("Activate");
		}

	}

	public void StartGame () {
		cam.SendMessage ("Activate");
		for (int i = 0; i < monkeys.Count; i++) {
			if (monkeys[i] != null) {
				monkeyUIPanels [i].SetActive (true);
				livesTexts [i].text = "x" + livesRemaining [i];
			}
		}
	}

	public void SpawnMonkey (int playerIndex) {
		monkeys.Add (Instantiate (monkeyPrefabs [playerIndex - 1], initialSpawnPoints [playerIndex].position, Quaternion.identity));
		numberOfMonkies++;
		livesRemaining [playerIndex - 1] = numberOfLives;
	}

	private IEnumerator RespawnPlayer (int playerNumber) {

		yield return new WaitForSeconds (2f);

		Instantiate (spawnPlatform, respawnPoints [nextPointIndex].position, Quaternion.identity);
		monkeys [playerNumber].transform.position = respawnPoints [nextPointIndex].position + Vector3.up * 2;
		monkeys [playerNumber].SendMessage ("Respawn");

		nextPointIndex++;
		if (nextPointIndex >= respawnPoints.Length) {
			nextPointIndex = 0;
		}

	}

	private void PlayerLost (int playerNumber) {
		gameOver [playerNumber].SetActive (true);

	}

	private void OnPlayerDeath (int playerIndex) {
		monkeys [playerIndex].SendMessage ("OnPlayerDeath");
		if (livesRemaining [playerIndex] <= 0) {
			PlayerLost (playerIndex);
		} else {
			livesRemaining [playerIndex]--;
			livesTexts [playerIndex].text = "x" + livesRemaining [playerIndex];
			StartCoroutine (RespawnPlayer (playerIndex));
		}
	}

	public void OnPlayerQuit (int playerIndex) {
		Destroy (monkeys [playerIndex]);
		monkeys.Remove (monkeys[playerIndex]);
		numberOfMonkies--;
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			int playerNumber = other.GetComponent<PlayerInput> ().playerNumber;
			OnPlayerDeath (playerNumber - 1);
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Prop") {
			trunkManager.SendMessage ("SpawnNewTrunk");
		}
	}
}
