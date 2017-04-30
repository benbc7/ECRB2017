using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbingCam : MonoBehaviour {
    
    public float currentSpeedPercentage;
    public float maxSpeed;
    public float maxSpeedScheduledTime;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentSpeedPercentage < 1)
            currentSpeedPercentage = (Time.time / maxSpeedScheduledTime);
        else
            currentSpeedPercentage = 1;
        
        transform.Translate(Vector3.up * (Time.deltaTime * maxSpeed * currentSpeedPercentage), Space.World);
    }
}
