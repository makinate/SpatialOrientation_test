using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotate target around player 
/// </summary>
public class RotateTarget : MonoBehaviour {
    private float azimuth;
    private float elevation;
    private GameObject player;
    private GameObject experimentManager;
    private bool practice;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Main Camera");
        experimentManager = GameObject.Find("ExperimentManager");
    }
    void Update()
    {
        // test if practice
        practice = experimentManager.GetComponent<ExperimentManager>().practice;
    }


    // Rotate target 
    public void Rotate()
    {
        azimuth = Random.Range(90, 270);
        elevation = Random.Range(-20, 20);
        // place object in front of camera
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        if (practice) { 
            // rotate it to a random position at least 90 deg from the camera forward view
            transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), azimuth);
            transform.RotateAround(transform.parent.position, new Vector3(1, 0, 0), elevation);
        }
        // in test trials only rotate around azimuth
        else
        {
            transform.localPosition = new Vector3(0, 0, 0.4f);
            transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), azimuth);
        }
    }

    // Set local rotation of target to player rotation
    public void FixTarget()
    {
        // place target straight ahead at 0 elevation 
        transform.position = new Vector3 (Camera.main.transform.position.x, 0 , Camera.main.transform.position.z) + Camera.main.transform.forward * 1.2f;

    }
}
