using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeManager : MonoBehaviour {

    public bool rightSide;
    public bool isOccupied;
    CircleCollider2D col;
    public Transform pivot;
    public ArborManager AM;
	// Use this for initialization
	void Start ()
    {
        col = GetComponent<CircleCollider2D>();
        AM = FindObjectOfType<ArborManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if (Input.GetKeyUp(KeyCode.Space) && isOccupied == true)
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

        if (isOccupied == false)
            {
             
         
                isOccupied = true;
                other.transform.position = pivot.transform.position; 
            }
        else
        {

        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (isOccupied == true)
        {


            isOccupied = false;
            
        }

    }

}

