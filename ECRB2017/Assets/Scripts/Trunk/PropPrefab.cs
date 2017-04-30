using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPrefab : PoolObject {

	private void Update () {
		if (transform.position.x < -0.27f) {
			transform.position = new Vector3 (-0.27f, transform.position.y, -4f);
			transform.localScale = new Vector3 (-1, 1, 1);
		} else if (transform.position.x > 0.27f) {
			transform.position = new Vector3 (0.27f, transform.position.y, -4f);
			transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Twig") {
			transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
			transform.position = new Vector3 (transform.position.x * -1, transform.position.y + Random.Range (-0.5f, 0.5f), -4f);
		}
	}
}
