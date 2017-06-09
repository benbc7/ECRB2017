using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] monkeyPrefabs;
	public GameObject spawnPlatform;

	public Transform[] initialSpawnPoints;
	public Transform[] respawnPoints;
	private int nextPointIndex;

	public AudioSource trackAudioSource;
	public AudioSource soundEffectSource;
	public AudioClip respawnClip;

	[Header ("Game Setup")]

	public bool playing;

	[Range (0, 11)]
	public int numberOfLives;

	public MonkeyInfo[] monkeyInfoArray;

	private GameObject theArborist;

	private GameObject cam;
	private TrunkManager trunkManager;
	private MenuManager menuManager;

	private void Awake () {
		trunkManager = FindObjectOfType<TrunkManager> ();
		menuManager = FindObjectOfType<MenuManager> ();
		cam = GameObject.Find ("Main Camera");
	}

	private void Update () {
		if (monkeyInfoArray [0].lives < 0 && monkeyInfoArray [1].lives < 0 && monkeyInfoArray [2].lives < 0 && playing) {
			SceneManager.LoadScene (0);
		}
	}

	public void StartGame () {
		cam.SendMessage ("Activate");
		menuManager.gameObject.SetActive (false);
		playing = true;
		StartCoroutine (FadeUpTrack ());
		for (int i = 0; i < 3; i++) {
			if (monkeyInfoArray[i].playing) {
				monkeyInfoArray [i].uiPanel.SetActive (true);
				monkeyInfoArray [i].livesText.text = "x" + monkeyInfoArray [i].lives;
			}
		}
	}

	private IEnumerator FadeUpTrack () {
		while (true) {
			trackAudioSource.volume += 0.01f;
			if (trackAudioSource.volume >= 1) {
				break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}

	public void SpawnMonkey (int playerIndex) {
		monkeyInfoArray[playerIndex - 1].monkey = (Instantiate (monkeyPrefabs [playerIndex - 1], initialSpawnPoints [playerIndex].position, Quaternion.identity));
		monkeyInfoArray [playerIndex - 1].playing = true;
		monkeyInfoArray [playerIndex - 1].lives = numberOfLives;
	}

	private IEnumerator RespawnPlayer (int playerNumber) {

		yield return new WaitForSeconds (2f);

		Instantiate (spawnPlatform, respawnPoints [nextPointIndex].position, Quaternion.identity);
		monkeyInfoArray [playerNumber].monkey.transform.position = respawnPoints [nextPointIndex].position + Vector3.up * 2;
		monkeyInfoArray [playerNumber].monkey.SendMessage ("Respawn");
		soundEffectSource.pitch = Random.Range (0.75f, 1.25f);
		soundEffectSource.PlayOneShot (respawnClip);

		nextPointIndex++;
		if (nextPointIndex >= respawnPoints.Length) {
			nextPointIndex = 0;
		}

	}

	private void PlayerLost (int playerNumber) {
		monkeyInfoArray [playerNumber].livesText.enabled = false;
		monkeyInfoArray [playerNumber].gameOverUI.SetActive (true);
		Destroy (monkeyInfoArray [playerNumber].monkey);
	}

	private void OnPlayerDeath (int playerIndex) {
		monkeyInfoArray [playerIndex].monkey.SendMessage ("OnPlayerDeath");
		monkeyInfoArray [playerIndex].lives--;
		if (monkeyInfoArray [playerIndex].lives < 0) {
			PlayerLost (playerIndex);
		} else {
			monkeyInfoArray [playerIndex].livesText.text = "x" + monkeyInfoArray [playerIndex].lives;
			StartCoroutine (RespawnPlayer (playerIndex));
		}
	}

	public void OnPlayerQuit (int playerIndex) {
		Destroy (monkeyInfoArray [playerIndex -1].monkey);
		monkeyInfoArray [playerIndex - 1].monkey = null;
		monkeyInfoArray [playerIndex - 1].playing = false;
		monkeyInfoArray [playerIndex - 1].lives = -1;
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

[System.Serializable]
public class MonkeyInfo {
	[HideInInspector]
	public GameObject monkey;
	public int playerNumber;
	[HideInInspector]
	public bool playing;
	[HideInInspector]
	public int lives = -1;
	public Text livesText;
	public GameObject uiPanel;
	public GameObject gameOverUI;

	public bool gameOver {
		get {
			return !(lives >= 0);
		}
	}
}