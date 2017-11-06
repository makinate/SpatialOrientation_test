using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// helper functions related to camera movement
/// </summary>
public class MoveCamera : MonoBehaviour {

    Camera cam; 
    public Transform sphere;
    public Transform target;
    public Texture starfield;
    public Texture room;
    public bool camLocked = false;
    public bool targetOnScreen;
    public float elevation;
    public float azimuth;
    public GameObject m_Fader;

    private Renderer rend;
    private Vector3 diffPlayerSphere;
    private bool playerSphereAligned;
    private GameObject player;
    private GameObject experimentManager;
    private GameObject target2;
    private bool practice;
    private bool faded = false;
    
    float z;
    float x;
    float y;


    void Awake()
    {
        player = GameObject.Find("Main Camera");
        cam = GetComponent<Camera>();
        //Find the fader object
        m_Fader = GameObject.Find("Fader");
        faded = true;
        m_Fader.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);

        target2 = GameObject.Find("Target 2");

        //Check if we found something
        if (m_Fader == null)
            Debug.LogWarning("No Fader object found on camera.");

    }

    void Start()
    {

        // get the renderer to swap the texture later
        rend = sphere.GetComponent<Renderer>();
        experimentManager = GameObject.Find("ExperimentManager");
    }

    // Update is called once per frame
    void Update () {

        practice = experimentManager.GetComponent<ExperimentManager>().practice;

        if (practice) {
            TestTargetinView();
        }
        else
        {
            targetOnScreen = false;
        }
        
        // calculate angular difference between camera and sphere rotation
        diffPlayerSphere = player.transform.eulerAngles - sphere.transform.eulerAngles;
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            LockCamera();
            RotateSphere();


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
        if ((diffPlayerSphere.y <= 10 || diffPlayerSphere.y >= 350) & (diffPlayerSphere.x <= 10 || diffPlayerSphere.x >= 350))
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

    // test if the target is in the center of the screen
    void TestTargetinView()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(target2.transform.position);
        targetOnScreen = screenPoint.z > 0 && screenPoint.x > 0.4 && screenPoint.x < 0.6 && screenPoint.y > 0.4 && screenPoint.y < 0.6;
        
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
        if (practice)
        {
            rend.material.mainTexture = starfield;
        }
        else
        {
            rend.material.mainTexture = room;
        }
        
    }
    public void fadeOut()
    {
        if (faded == false)
        {
            faded = true;
            m_Fader.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            
        }
    }

    public void fadeIn()
    {
        if (faded == true)
        {
            faded = false;
            m_Fader.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
            
        }
    }

}
