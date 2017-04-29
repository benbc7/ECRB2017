using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrefab : PoolObject {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Node") {
			transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
			transform.position = new Vector3 (transform.position.x * -1, transform.position.y, transform.position.z);
		}
	}
}
