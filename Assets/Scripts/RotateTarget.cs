using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTarget : MonoBehaviour {
    public int angle;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        


        if (Input.GetKeyDown(KeyCode.R)) {
            setAngle();
            transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), angle);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            //setAngle();
            angle = 5;
            transform.RotateAround(transform.parent.position, new Vector3(0, -1, 0), angle);
        }

    }
    void setAngle()
    {
        angle = Random.Range(0, 360);
    }
}
