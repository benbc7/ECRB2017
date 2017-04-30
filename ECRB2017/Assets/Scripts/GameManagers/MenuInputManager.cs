using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent (typeof (MenuManager))]
public class MenuInputManager : MonoBehaviour {

	private Player[] players = new Player[4];
	private MenuManager menuManager;

	private void Start () {
		menuManager = GetComponent<MenuManager> ();

		for (int i = 0; i < 4; i++) {
			players [i] = ReInput.players.GetPlayer (i);
		}
	}

	private void Update () {
		for (int i = 0; i < 4; i++) {
			GetInput (i);
		}
	}

	private void GetInput (int i) {
		if (players [i].GetButtonDown ("Start")) {
			menuManager.PlayerInput (i, "Start");
		}
		if (players [i].GetButtonDown ("Select")) {
			menuManager.PlayerInput (i, "Select");
		}
		if (players [i].GetButtonDown ("Back")) {
			menuManager.PlayerInput (i, "Back");
		}
		if (players [i].GetButtonDown ("Up")) {
			menuManager.PlayerInput (i, "Up");
		}
		if (players [i].GetButtonDown ("Down")) {
			menuManager.PlayerInput (i, "Down");
		}
		if (players [i].GetButtonDown ("Right")) {
			menuManager.PlayerInput (i, "Right");
		}
		if (players [i].GetButtonDown ("Left")) {
			menuManager.PlayerInput (i, "Left");
		}
	}
}
