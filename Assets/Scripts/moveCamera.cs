using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public GameObject player;
    public Transform sphere;
    public Transform target;

    public Texture starfield;
    public Texture room;

    private Renderer rend;
    private bool camLocked = false;

    void Start()
    {

        // get the renderer to swap the texture later
        rend = sphere.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            rend.material.mainTexture = room;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            rend.material.mainTexture = starfield;

        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            LockCamera();
            Debug.Log("Camera Rotation: " + player.transform.eulerAngles);
            Debug.Log("Sphere Rotation: " + sphere.transform.eulerAngles);
            Debug.Log("Target Rotation: " + target.transform.eulerAngles);
            Debug.Log("Camera-Sphere Rotation: " + (player.transform.eulerAngles - sphere.transform.eulerAngles));
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
            player.transform.localEulerAngles = sphere.transform.localEulerAngles;
            camLocked = true;
        }

    }
}
