using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotate the sphere
/// </summary>
public class RotateSphere : MonoBehaviour {

    
    public Texture starfield;
    public Texture room;
    public Texture hallway;
    public float elevation;
    public float azimuth;

    private Renderer rend;
    private bool practice;
    private GameObject player;
    private GameObject sphere;
    private GameObject em;
    private Shader insideOut;
    private Shader wireFrame;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Main Camera");
        sphere = GameObject.Find("Sphere 1");
        em     = GameObject.Find("ExperimentManager");
        
        // get the renderer to swap the texture later
        rend = sphere.GetComponent<Renderer>();

        insideOut = Shader.Find("Insideout");
        wireFrame = Shader.Find("HoloToolkit/Wireframe");
    }
    private void Update()
    {
        // test if practice
        practice = em.GetComponent<ExperimentManager>().practice;
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
    // apply texture to sphere depending on practice or test trials
    public void SetTexture()
    {
        Debug.Log("Setting Texture");
        if (practice)
        {
            Debug.Log("HALLWAY");
            rend.material.mainTexture = hallway;
        }
        else
        {
            Debug.Log("ROOM");
            rend.material.mainTexture = room;
        }
    }

    // sawps shader 
    public void SwapShader()
    {
        Debug.Log("Swapping shader");
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
