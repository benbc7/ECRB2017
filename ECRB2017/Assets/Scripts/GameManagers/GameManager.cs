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
	private int numberOfMonkies;

	private void Awake () {
		cameraManager = FindObjectOfType<CameraManager> ();
	}

	public void SpawnMonkey (int playerIndex) {
		monkeys.Add (Instantiate (monkeyPrefabs [playerIndex - 1], initialSpawnPoints [playerIndex].position, Quaternion.identity));
	}

	public void StartGame () {

	}
}
