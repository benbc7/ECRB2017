using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterWait : MonoBehaviour {

	public float waitTime;

	private void Start () {
		StartCoroutine (WaitForSeconds ());
	}

	private IEnumerator WaitForSeconds () {
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}
}
