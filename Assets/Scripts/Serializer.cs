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
    public GameObject playerCam; // the gameobject whose rotation we want to know
    
    // define public variables for experiment
    public string sbj     = "sbj00";
    public string vision  = "control";
    public string explore = "active-multi";
    public string training = "training";

    // private variables
    private string  myfilename;
    private string  timeStamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
    private string  tempEvent;
    private float   tempTime;      
    private TextWriter sw;
    private GameObject experimentManager;
    private Vector3 tempPosition;     // position
    private Vector3 tempRotation;     // rotation

    // prep file
    void Start () {
        experimentManager = GameObject.Find("ExperimentManager");
        tempPosition = player.transform.position;
        tempRotation = playerCam.transform.rotation.eulerAngles;
        tempTime     = 0.0f;
        tempEvent    = "idle";

        string subPath = "Data"; 

        bool exists = System.IO.Directory.Exists(subPath);

        if (!exists)
            System.IO.Directory.CreateDirectory(subPath);

        myfilename = "Data/"+ sbj + "_" + vision + "_" + explore + "_" + timeStamp + ".csv";

        sw = new StreamWriter(myfilename);

        // write a header
        string header = "x, y, z, pitch, yaw, roll, event, event";
        sw.WriteLine(header);
    }
	
	// log the data
	void Update () {
        tempPosition = player.transform.position;
        tempRotation = playerCam.transform.rotation.eulerAngles;
        tempTime     = tempTime + Time.fixedDeltaTime;
        tempEvent    = experimentManager.GetComponent<ExperimentManager>().currentState.ToString();

        // write position once every Uptdate()
        WriteToFile();
        
    }

    // write position, rotation, events and time stamp to file
    void WriteToFile()
    {
        string output = tempPosition.x + "," + tempPosition.y + "," + tempPosition.z + "," + tempRotation.x + "," + tempRotation.y + "," + tempRotation.z + "," + tempEvent + "," + tempTime;
        sw.WriteLine(output);
    }

    // close file on quit
    void OnApplicationQuit()
    {
        sw.Close();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
