using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrefab : PoolObject {

	private nodeManager nodeManager;
	private float timer;
	private bool stillSettling;

	private void Awake () {
		nodeManager = GetComponent<nodeManager> ();
	}

	public override void OnObjectReuse () {
		base.OnObjectReuse ();

		nodeManager.col.enabled = true;
		nodeManager.isOccupied = false;
	}

	private void Update () {

		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer <= 0) {
			stillSettling = false;
		}

		if (transform.position.x < 0 && transform.position.x != -0.27f) {
			transform.position = new Vector3 (-0.27f, transform.position.y, -3f);
			transform.localScale = new Vector3 (-1, 1, 1);
		} else if (transform.position.x > 0 && transform.position.x != 0.27f) {
			transform.position = new Vector3 (0.27f, transform.position.y, -3f);
			transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	private void ResetTimer () {
		timer = 3f;
		stillSettling = true;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Node" && stillSettling) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
			transform.position = new Vector3 (transform.position.x * -1, transform.position.y + Random.Range (-1f, 1f), -3f);
			nodeManager.rightSide = !nodeManager.rightSide;
		}
	}
}
