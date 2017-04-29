using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchMovement : MonoBehaviour {
    public float moveSpeed;
    public float rotSpeed;
    public bool isLocked;
    public bool isPositioned;

    public SpriteRenderer sr;
	// Use this for initialization
	void Start ()
    {
        AlphaTransition(false);

	}

    // Update is called once per frame
    void Update() {
        if (isPositioned)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isLocked = true;

                AlphaTransition(true);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isPositioned = false;
            }
        }

        if (!isLocked)
        {
            if (!isPositioned)
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
        public void AlphaTransition (bool real)
    {
        if (real == true)
        {
            Debug.Log("going hard");
            sr.color += new Color (255, 255, 255, 255);

        }
       else
        {

            Debug.Log("going clear");
            sr.color += new Color(125, 125, 125, 125);

        }
    }
    
}
