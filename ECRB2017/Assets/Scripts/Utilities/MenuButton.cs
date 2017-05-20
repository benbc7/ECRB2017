using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public enum  ButtonType { start, exit};
	public ButtonType buttonType;

	public Sprite[] sprites;

	private bool buttonSelected;
	private Image image;
	private MenuManager menuManager;

	private void Start () {
		image = GetComponent<Image> ();
		SetUpCollider ();
		menuManager = FindObjectOfType<MenuManager> ();
	}

	private void Confirm () {
		if (buttonType == ButtonType.start) {
			menuManager.SendMessage ("StartGame");
		} else if (buttonType == ButtonType.exit) {
			menuManager.SendMessage ("ExitGame");
		}
	}

	private void SelectButton () {
		image.sprite = sprites [1];
	}

	private void DeselectButton () {
		image.sprite = sprites [0];
	}

	private void SetUpCollider () {
		Vector3 colliderSize = new Vector3 (image.rectTransform.rect.width, image.rectTransform.rect.height, 1);
		BoxCollider collider = gameObject.AddComponent<BoxCollider> ();
		collider.size = colliderSize;
	}
}
