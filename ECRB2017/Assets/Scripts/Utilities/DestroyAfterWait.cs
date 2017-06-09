using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterWait : MonoBehaviour {

	public float waitTime;
	public bool decreaseOpacity;

	private SpriteRenderer spriteRenderer;
	private Color currentColor;

	private void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		StartCoroutine (WaitForSeconds ());
		if (decreaseOpacity) {
			StartCoroutine (DecreaseOpacityWithWaitTime ());
		}
	}

	private IEnumerator DecreaseOpacityWithWaitTime () {
		currentColor = spriteRenderer.color;
		while (true) {
			currentColor.a -= Time.deltaTime / (waitTime / 1.5f);
			spriteRenderer.color = currentColor;
			if (currentColor.a <= 0) {
				break;
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}

	private IEnumerator WaitForSeconds () {
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}
}
