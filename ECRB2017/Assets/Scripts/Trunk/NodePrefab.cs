using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrefab : PoolObject {

	private nodeManager nodeManager;

	private void Start () {
		nodeManager = GetComponent<nodeManager> ();
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Node") {
			transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
			nodeManager.rightSide = !nodeManager.rightSide;
			transform.position = new Vector3 (transform.position.x * -1, transform.position.y + Random.Range (-1f, 1f), transform.position.z);
		}
	}
}
