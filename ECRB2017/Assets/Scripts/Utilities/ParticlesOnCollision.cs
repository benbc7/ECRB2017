using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnCollision : MonoBehaviour {

	new private ParticleSystem particleSystem;
	private float timer;
	private bool ready;

	private void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
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
		}
	}

	private void OnCollisionEnter2D (Collision2D collision) {
		print ("Hit");
		if (collision.gameObject.tag == "Player") {
			print ("Player");
			particleSystem.Play ();
		}
	}
}
