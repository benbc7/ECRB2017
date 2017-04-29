using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkManager : MonoBehaviour {

	public GameObject trunkPrefab;

	public SetPiecePrefabs setPiecePrefabs;

	private Transform stump;

	private void Start () {
		PoolManager.instance.CreatePool (trunkPrefab, 5);
		PoolManager.instance.CreatePool (setPiecePrefabs.nodePrefab, 20);
		PoolManager.instance.CreatePool (setPiecePrefabs.holePrefab, 5);
		for (int i = 0; i < setPiecePrefabs.tinyBranchPrefabs.Length; i++) {
			PoolManager.instance.CreatePool (setPiecePrefabs.tinyBranchPrefabs [i], 20);
		}
		for (int i = 0; i < setPiecePrefabs.knotPrefabs.Length; i++) {
			PoolManager.instance.CreatePool (setPiecePrefabs.knotPrefabs [i], 40);
		}
		stump = GameObject.Find ("Stump").transform;
		stump.GetComponent<TrunkPrefab> ().OnObjectReuse ();

		InitializeTrunk ();
	}

	private void InitializeTrunk () {
		for (int i = 0; i < 5; i++) {
			PoolManager.instance.ReuseObject (trunkPrefab, new Vector3 (stump.position.x, stump.position.y + (4 * (i + 1)), -1), Quaternion.identity);
		}
	}


}

[System.Serializable]
public class SetPiecePrefabs {

	public GameObject nodePrefab;
	public GameObject holePrefab;
	public GameObject[] tinyBranchPrefabs;
	public GameObject[] knotPrefabs;

}
