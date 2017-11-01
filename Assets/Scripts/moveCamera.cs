using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public GameObject player;
    public Transform sphere;
    public Transform target;
    public Texture starfield;
    public Texture room;
    public bool camLocked = false;

    private Renderer rend;
    private Vector3 diff_player_sphere;
    private bool playerSphereAligned;
    private bool sfTexture = false;
    

    void Start()
    {

        // get the renderer to swap the texture later
        rend = sphere.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {

        // calculate angular difference between camera and sphere rotation
        diff_player_sphere = player.transform.eulerAngles - sphere.transform.eulerAngles;

        ViewInRange();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetTexture(); 
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            LockCamera();
            RotateSphere();
            //Debug.Log("Camera Rotation: " + player.transform.eulerAngles);
            //Debug.Log("Sphere Rotation: " + sphere.transform.eulerAngles);
            //Debug.Log("Target Rotation: " + target.transform.eulerAngles);
            //Debug.Log("Camera-Sphere Rotation: " + diff_player_sphere);

            if (camLocked)
            {
                // rotate camera to sphere
                player.transform.localEulerAngles = sphere.transform.localEulerAngles;
                // TO DO: switch off input from Oculus head motion
            }
            else if (!camLocked)
            {
                // TO DO: switch Oculus head motion back on
               
            }
        }

        
    }

    // rotate sphere in a random az and el around player
    public void RotateSphere()
    {
        int azimuth = Random.Range(0, 360);
        int elevation = Random.Range(-20, 20);
        sphere.transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), azimuth);
    }

    // test if player and sphere rotation are within the same ballpark
    void ViewInRange()
    {
        if ((diff_player_sphere.y <= 10 || diff_player_sphere.y >= 350) & (diff_player_sphere.x <= 10 || diff_player_sphere.x >= 350))
        {
            //Debug.Log("TARGET IN RANGE" + diff_player_sphere);
            playerSphereAligned = true;

        }
        else
        {
            //Debug.Log("target out of range");
            playerSphereAligned = false;
        }
    }


    void LockCamera()
    {
        if (camLocked)
        {
            //unlock camera from sphere
            camLocked = false;
        }
        else if (!camLocked)
        {
            // lock camera view to sphere
            //player.transform.localEulerAngles = sphere.transform.localEulerAngles;
            camLocked = true;
        }

    }

    public void SetTexture()
    {
        if (!sfTexture)
        {
            rend.material.mainTexture = starfield;
            sfTexture = true;
        }
        else
        {
            rend.material.mainTexture = room;
            sfTexture = false;
        }
        
    }

}
