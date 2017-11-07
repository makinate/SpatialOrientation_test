using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// helper functions related to camera movement
/// </summary>
public class RotateCamera : MonoBehaviour {

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

    public Shader insideOut;
    public Shader wireFrame;

    private Renderer rend;
    public Vector3 diffPlayerSphere;
    private bool playerSphereAligned;
    private GameObject player;
    private GameObject experimentManager;
    private GameObject target2;
    private bool practice;
    private bool faded = true;
    
    float z;
    float x;
    float y;

    void Start()
    {
        player = GameObject.Find("Main Camera");
        cam = GetComponent<Camera>();

        //Find the fader object
        m_Fader = GameObject.Find("Fader");
        m_Fader.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1); // Fade to black

        target2 = GameObject.Find("Target 2");

        // get the renderer to swap the texture later
        rend = sphere.GetComponent<Renderer>();
        insideOut = Shader.Find("Insideout");
        wireFrame = Shader.Find("HoloToolkit/Wireframe");
        experimentManager = GameObject.Find("ExperimentManager");
    }

    // Update is called once per frame
    void Update () {
        // test if practice
        practice = experimentManager.GetComponent<ExperimentManager>().practice;

        TestTargetinView();
        
        // calculate angular difference between camera and sphere rotation
        diffPlayerSphere = player.transform.localEulerAngles - sphere.transform.localEulerAngles;     
    }

 
    // test if player and sphere rotation are within the same ballpark
    void ViewInRange()
    {
        if (!practice) { 
            if ((diffPlayerSphere.y <= 10 || diffPlayerSphere.y >= 350) & (diffPlayerSphere.x <= 10 || diffPlayerSphere.x >= 350))
            {
                Debug.Log("TARGET IN RANGE" + diffPlayerSphere);
                playerSphereAligned = true;
            }
            else
            {
                Debug.Log("target out of range");
                playerSphereAligned = false;
            }
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

    // apply texture to sphere depending on practice or test trials
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

    // fadeout function
    public void FadeOut()
    {
        if (faded == false)
        {
            faded = true;
            m_Fader.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            
        }
    }

    // fadein function
    public void FadeIn()
    {
        if (faded == true)
        {
            faded = false;
            m_Fader.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
            
        }
    }

    // sawps shader 
    public void SwapShader()
    {
        if (rend.material.shader == insideOut)
        {
            rend.material.shader = wireFrame;
        }
        else
        {
            rend.material.shader = insideOut;
        }
    }

}
