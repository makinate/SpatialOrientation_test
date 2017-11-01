using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Serializer: Logs position, rotation, events, and timestamps and writes them to a file
/// </summary>
/// 
public class Serializer : MonoBehaviour {

    public GameObject player; // the gameObject whose position we want to know
    public GameObject sphere;
    // define public variables for experiment
    public string sbj     = "sbj00";
    public string vision  = "control";
    public string explore = "active-multi";
    public string training = "training";

    // private variables
    private string  myfilename;
    private string  timeStamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
    private string  currentState;
    private float   tempTime;      
    private TextWriter sw;
    private GameObject experimentManager;
    private Vector3 tempPlayerPosition;     // position
    private Vector3 tempPlayerRotation;     // rotation
    private Vector3 tempSpherePosition;     // position
    private Vector3 tempSphereRotation;     // rotation
    // prep file
    void Start () {
        experimentManager = GameObject.Find("ExperimentManager");
        tempPlayerPosition = player.transform.position;
        tempPlayerRotation = player.transform.rotation.eulerAngles;
        tempSpherePosition = sphere.transform.position;
        tempSphereRotation = sphere.transform.rotation.eulerAngles;
        tempTime = 0.0f;
        currentState = experimentManager.GetComponent<ExperimentManager>().currentState.ToString();

        string subPath = "Data"; 

        bool exists = System.IO.Directory.Exists(subPath);

        if (!exists)
            System.IO.Directory.CreateDirectory(subPath);

        myfilename = "Data/"+ sbj + "_" + vision + "_" + explore + "_" + timeStamp + ".csv";

        sw = new StreamWriter(myfilename);

        // write a header
        string header = "x, y, z, playerPitch, playerYaw, playerRoll, sphere.x, sphere.y, sphere.z, spherePitch, sphereYaw, sphereRoll, event, time";
        sw.WriteLine(header);
    }
	
	// log the data
	void Update () {
        tempPlayerPosition = player.transform.position;
        tempPlayerRotation = player.transform.rotation.eulerAngles;
        tempSpherePosition = sphere.transform.position;
        tempSphereRotation = sphere.transform.rotation.eulerAngles;
        tempTime     = tempTime + Time.fixedDeltaTime;
        currentState = experimentManager.GetComponent<ExperimentManager>().currentState.ToString();

        // write position once every Uptdate()
        WriteToFile();
        
    }

    // write position, rotation, events and time stamp to file
    void WriteToFile()
    {
        string output = tempPlayerPosition.x + "," + tempPlayerPosition.y + "," + tempPlayerPosition.z + "," + 
                        tempPlayerRotation.x + "," + tempPlayerRotation.y + "," + tempPlayerRotation.z + "," +
                        tempSpherePosition.x + "," + tempSpherePosition.y + "," + tempSpherePosition.z + "," +
                        tempSphereRotation.x + "," + tempSphereRotation.y + "," + tempSphereRotation.z + "," +
                        currentState + "," + tempTime;
        sw.WriteLine(output);
    }

    // close file on quit
    void OnApplicationQuit()
    {
        sw.Close();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
