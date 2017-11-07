using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotate the sphere
/// </summary>
public class RotateSphere : MonoBehaviour {
    public Transform sphere;

    private bool practice;
    private GameObject player;
    public float elevation;
    public float azimuth;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Main Camera");
    }

    // rotate sphere in a random az and el around player
    public void Rotate()
    {
        if (!practice)
        {
            // make sure that sphere is rotated straight ahead at start
            sphere.transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), 0);
            sphere.transform.RotateAround(player.transform.position, new Vector3(1, 0, 0), 0);

            // define random rotation angles for azimuth and elevation
            azimuth += Random.Range(90, 270); // rotate at least 90 deg away from first camera view
            elevation = Random.Range(-20, 20);
            sphere.transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), azimuth);    // rotate bearing/azimuth
            sphere.transform.RotateAround(player.transform.position, new Vector3(1, 0, 0), elevation);  // roatate elavation
        }
    }

    // rotate  camera and sphere to origin
    public void ResetSphere()
    {
        azimuth = Random.Range(0, 360);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        sphere.transform.localEulerAngles = new Vector3(0, azimuth, 0);
    }

}
