﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPrefab : PoolObject {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Twig") {
			transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
			transform.position = new Vector3 (transform.position.x * -1, transform.position.y + Random.Range (-0.5f, 0.5f), transform.position.z);
		}
	}
}