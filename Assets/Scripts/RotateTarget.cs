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
	
    void setAngle()
    {
        
        azimuth = Random.Range(90, 270);
        elevation = Random.Range(-20, 20);
    }

    public void Rotate()
    {
        setAngle();
        // place object in front of camera
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        // rotate it to a random position at least 90 deg from the camera forward view
        transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), azimuth);
        transform.RotateAround(transform.parent.position, new Vector3(1, 0, 0), elevation);
    }

    // Set local rotation of target to player rotation
    public void FixTarget()
    {
        // transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        // place target straight ahead at 0 elevation 
        transform.position = new Vector3 (Camera.main.transform.position.x, 0 , Camera.main.transform.position.z) + Camera.main.transform.forward * 1.2f;

    }
}
