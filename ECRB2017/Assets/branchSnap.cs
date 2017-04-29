using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class branchSnap : MonoBehaviour {

    public BranchMovement BM;
	// Use this for initialization
	void Start ()
    {
        //BM = GetComponentInParent<BranchMovement>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trig enter");
        if (other.name == "spawnPoint")
        {
            transform.rotation = other.transform.rotation;
            transform.position = other.transform.position;
            BM.isLocked = true;
        }
    }
}
