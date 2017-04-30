using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeManager : MonoBehaviour {

    public bool rightSide;
    public bool leftSide;
    public bool isOccupied;
    CircleCollider2D col;
    public Transform pivot;
    public ArborManager AM;
	// Use this for initialization
	void Start ()
    {
        col = GetComponent<CircleCollider2D>();
        AM = GameObject.Find("ArborManager").GetComponent<ArborManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if (Input.GetKeyDown(KeyCode.Space) && isOccupied == true)
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
        if (other.tag == "Branch")
        {
            if (rightSide && other.gameObject.GetComponent<BranchMovement>().facingRight == true)
            {
                isOccupied = true;
                other.transform.position = pivot.transform.position;
            }
            else if (!rightSide && other.gameObject.GetComponent<BranchMovement>().facingRight == false)
            {
                isOccupied = true;
                other.transform.position = pivot.transform.position;
            }
        }
    }
}

