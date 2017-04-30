using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkPrefab : PoolObject {

	private TrunkManager trunkManager;
	private SetPiecePrefabs setPiecePrefabs;

	private void Awake () {
		trunkManager = FindObjectOfType<TrunkManager> ();
		if (trunkManager != null) {
			setPiecePrefabs = trunkManager.setPiecePrefabs;
		}
	}

	public override void OnObjectReuse () {
		base.OnObjectReuse ();

		SpawnNodes ();

		SpawnTwigs ();

		SpawnKnots ();

		SpawnHole ();
	}

	private void SpawnNodes () {
		int numberOfNodes = Random.Range (2, 5);

		Vector3 position = Vector3.zero;
		position.x = 0.27f;

		for (int i = 0; i < numberOfNodes; i++) {
			position.x *= -1;
			position.y = Random.Range (transform.position.y - 1.75f, transform.position.y + 1.75f);
			position.z = -4f;
			var node = PoolManager.instance.ReuseObject (setPiecePrefabs.nodePrefab, position, Quaternion.identity);
			if (position.x < 0) {
				node.transform.localScale = new Vector3 (-1, 1, 1);
				node.GetComponent<nodeManager> ().rightSide = false;
			}
		}
	}

	private void SpawnTwigs () {
		int numberOfTwigs = Random.Range (4, 8);

		Vector3 position = Vector3.zero;
		position.x = 0.44f;

		for (int i = 0; i < numberOfTwigs; i++) {
			position.x *= -1;
			position.y = Random.Range (transform.position.y - 1.75f, transform.position.y + 1.75f);
			position.z = -4f;
			var twig = PoolManager.instance.ReuseObject (setPiecePrefabs.tinyBranchPrefabs [Random.Range (0, setPiecePrefabs.tinyBranchPrefabs.Length)], position, Quaternion.identity);
			if (position.x < 0) {
				twig.transform.localScale = new Vector3 (-1, 1, 1);
			}
		}
	}

	private void SpawnKnots () {
		int numberOfKnots = Random.Range (5, 11);

		Vector3 position = Vector3.zero;
		Vector3 rotation = Vector3.zero;

		for (int i = 0; i < numberOfKnots; i++) {
			position.x = Random.Range (-0.4f, 0.4f);
			position.y = Random.Range (transform.position.y - 1.75f, transform.position.y + 1.75f);
			position.z = -4f;
			rotation.z = Random.Range (-2, 2) * 90;
			var knot = PoolManager.instance.ReuseObject (setPiecePrefabs.knotPrefabs [Random.Range (0, setPiecePrefabs.knotPrefabs.Length)], position, Quaternion.Euler (rotation));
			if (position.x < 0) {
				knot.transform.localScale = new Vector3 (-1, 1, 1);
			}
		}
	}

	private void SpawnHole () {
		int numberOfHoles = (Random.Range (0, 10) == 0) ? 1 : 0;

		if (numberOfHoles == 1) {
			Vector3 position = Vector3.zero;

			position.x = Random.Range (-0.2f, 0.2f);
			position.y = Random.Range (transform.position.y - 1.75f, transform.position.y + 1.75f);
			position.z = -5f;
			var hole = PoolManager.instance.ReuseObject (setPiecePrefabs.holePrefab, position, Quaternion.identity);
			if (position.x < 0) {
				hole.transform.localScale = new Vector3 (-1, 1, 1);
			}
		}
	}
}
