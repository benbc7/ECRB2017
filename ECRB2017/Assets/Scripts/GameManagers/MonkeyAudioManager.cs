using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAudioManager : MonoBehaviour {

	public AudioClip whoosh;
	public AudioClip groundPound;
	public AudioClip dying;
	public AudioClip[] hits;
	public AudioClip[] monkeyChatterClips;

	private AudioSource audioSource;

	private void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayMonkeyChatter () {
		if (!audioSource.isPlaying) {
			audioSource.pitch = Random.Range (0.75f, 1.25f);
		}
		audioSource.PlayOneShot (monkeyChatterClips [Random.Range (0, monkeyChatterClips.Length)]);
	}

	public void PlayWhoosh () {
		if (!audioSource.isPlaying) {
			audioSource.pitch = Random.Range (0.75f, 1.25f);
		}
		audioSource.PlayOneShot (whoosh);
	}

	public void PlayHits () {
		if (!audioSource.isPlaying) {
			audioSource.pitch = Random.Range (0.75f, 1.25f);
		}
		audioSource.PlayOneShot (hits[Random.Range (0, hits.Length)]);
	}

	public void PlayGroundPound () {
		if (!audioSource.isPlaying) {
			audioSource.pitch = Random.Range (0.75f, 1.25f);
		}
		audioSource.PlayOneShot (groundPound);
	}

	public void PlayDying () {
		if (!audioSource.isPlaying) {
			audioSource.pitch = Random.Range (0.75f, 1.25f);
		}
		audioSource.PlayOneShot (dying);
	}
}
