using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAtRandomIntervals : MonoBehaviour {

	private Animator animator;

	private void Start () {
		animator = GetComponent<Animator> ();
		StartCoroutine (BlinkAtRandom ());
	}

	private IEnumerator BlinkAtRandom () {
		while (true) {
			yield return new WaitForSeconds (Random.Range (0.5f, 4f));
			animator.SetTrigger ("Blink");
		}
	}
}
