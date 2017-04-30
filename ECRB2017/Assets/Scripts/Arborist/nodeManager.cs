using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;
public class nodeManager : MonoBehaviour {

    public bool rightSide;
    public bool isOccupied;
    public CircleCollider2D col;
    public Transform pivot;
    public ArboristController AC;
    public Sprite normalNode;
    public Sprite highlightedNode;
    public SpriteRenderer SR;

    Player joystick;
    // Use this for initialization
    void Start ()
    {
        joystick = ReInput.players.GetPlayer(0);
        col = GetComponent<CircleCollider2D>();
        AC = FindObjectOfType<ArboristController>();
        SR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isOccupied == true)
            SR.sprite = highlightedNode;
        else
            SR.sprite = normalNode;


        if (joystick.GetButtonUp("PlaceBranch") && isOccupied == true)
        {
            col.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOccupied == true)
        {
            isOccupied = false;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "ArboristController")
        {
            if (isOccupied == false)
            {
                isOccupied = true;
                //other.transform.position = pivot.transform.position;
            }
            else
            {

            }
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "ArboristController")
        {
            if (isOccupied == true)
            {


                isOccupied = false;

            }
        }

    }

}

