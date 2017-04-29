using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

    MonkeyPlayer monkey;

    Player controller;

	// Use this for initialization
	void Start () {
        monkey = GetComponent<MonkeyPlayer> ();
        controller = ReInput.players.GetPlayer (0);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 directionalInput = new Vector2 (controller.GetAxisRaw ("Horizontal"), controller.GetAxisRaw ("Vertical"));
        monkey.SetDirectionalInput (directionalInput);

        if (controller.GetButtonDown ("Jump")) {
            monkey.OnJumpInputDown ();
        }
        if (controller.GetButtonUp ("Jump")) {
            monkey.OnJumpInputUp ();
        }
    }
}
