using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTarget : MonoBehaviour {
    public float azimuth;
    public float elevation;

    private GameObject player;
 
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
        


        if (Input.GetKeyDown(KeyCode.R)) {
            setAngle();
            transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), azimuth);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            //setAngle();
            azimuth = 5;
            transform.RotateAround(transform.parent.position, new Vector3(0, -1, 0), azimuth);
        }

    }
    void setAngle()
    {
        azimuth = Random.Range(0, 360);
        elevation = Random.Range(-20, 20);
    }

    public void Rotate()
    {
        setAngle();
        transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), azimuth);
        transform.RotateAround(transform.parent.position, new Vector3(1, 0, 0), elevation);
    }

    // Set local rotation of target to player rotation
    public void FixTarget()
    {
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;

    }
}
