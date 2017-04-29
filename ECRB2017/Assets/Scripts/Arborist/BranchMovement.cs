using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchMovement : MonoBehaviour {
    public float moveSpeed;
    public float rotSpeed;
    public bool isLocked;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocked)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * (moveSpeed / 100), Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * (moveSpeed / 100), Space.World);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * (moveSpeed / 100), Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * (moveSpeed / 100), Space.World);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.forward * rotSpeed);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.back * rotSpeed);
            }
        }
    }
}
