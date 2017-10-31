using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotator : MonoBehaviour {
    private bool faded = false; 
    float z;
    float x;
    float y;
    public float euler;
    public float azimuthRot;
    private GameObject m_Fader; 

    void Awake()
    {

        //Find the fader object
        m_Fader = GameObject.Find("Fader");
        faded = true;
        m_Fader.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);

        //Check if we found something
        if (m_Fader == null)
            Debug.LogWarning("No Fader object found on camera.");

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            fadeInOut();
            z = transform.eulerAngles.z;
            x = transform.eulerAngles.x;
            y = transform.eulerAngles.y;
            euler = Random.Range(0, 360);

            Vector3 desiredRot = new Vector3(x, y + euler, z);

            gameObject.transform.rotation = Quaternion.Euler(desiredRot);
            Debug.Log("Rotate to random view (around vertical axis only)");
            
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            //gameObject.transform.rotation = Random.rotation;

            z = transform.eulerAngles.z;
            x = transform.eulerAngles.x;
            y = transform.eulerAngles.y;
            azimuthRot = Random.Range(-20, 20);
            Vector3 desiredRot = new Vector3(x + azimuthRot, y , z);

            gameObject.transform.rotation = Quaternion.Euler(desiredRot);
            Debug.Log("Rotate to random view (around horizontal axis only)");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            //gameObject.transform.rotation = Random.rotation;

            z = transform.eulerAngles.z;
            x = transform.eulerAngles.x;
            y = transform.eulerAngles.y;
            euler = Random.Range(0, 360);
            azimuthRot = Random.Range(-20, 20);
            Vector3 desiredRot = new Vector3(x + azimuthRot, y + euler, z);

            gameObject.transform.rotation = Quaternion.Euler(desiredRot);
            Debug.Log("Rotate to random view around vertical AND horizontal axis");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            //gameObject.transform.rotation = Random.rotation;

           
            Vector3 desiredRot = new Vector3(0,0,0);

            gameObject.transform.rotation = Quaternion.Euler(desiredRot);
            Debug.Log("Reset rotation");
        }
    }

    // fade camera
    void fadeInOut()
    {
        // find camera 
        // toggle 
        if(faded == true)
        {
            faded = false;
            Debug.Log("FADE IN");
            m_Fader.GetComponent<Renderer>().material.color = new Color(1,0,0,0);

        }
        else if (faded == false)
        {
            faded = true;
            m_Fader.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            Debug.Log("FADE OUT");
        }
    }
}
