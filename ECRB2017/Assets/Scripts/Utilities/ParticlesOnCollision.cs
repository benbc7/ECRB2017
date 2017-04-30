using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnCollision : MonoBehaviour {

	public AudioClip[] leaves;

	private AudioSource audioSource;
	new private ParticleSystem particleSystem;
	private float timer;
	private bool ready;

	private void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
		audioSource = GetComponent<AudioSource> ();
	}

	private void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer <= 0) {
			ready = true;
		}
	}

	private void PlayParticleSystem () {
		if (ready) {
			timer = 3f;
			ready = false;
			particleSystem.Play ();
			if (!audioSource.isPlaying) {
				audioSource.pitch = Random.Range (0.75f, 1.25f);
			}
			audioSource.PlayOneShot (leaves[Random.Range (0, leaves.Length)]);
		}
	}

	private void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			print ("Player");
			particleSystem.Play ();
		}
	}
}
