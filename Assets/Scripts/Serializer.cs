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
    public string sbj     = "XX";
    public string vision  = "control";
    public string explore = "active-multi";

    // private variables
    private string  myfilename;
    private string  timeStamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
    private string  currentState;
    private float   tempTime;
    private float   totalTime;
    private float   trial;
    private bool    practice;
    
    private TextWriter sw;
    private GameObject experimentManager;
    private Vector3 tempPlayerPosition;     // position
    private Vector3 tempPlayerRotation;     // rotation
    private Vector3 tempSpherePosition;     // position
    private Vector3 tempSphereRotation;     // rotation
    private Vector3 tempDiffPlayerSphere;
    
    // prep file
    void Start () {
        experimentManager = GameObject.Find("ExperimentManager");
        trial = experimentManager.GetComponent<ExperimentManager>().counter;
        practice = experimentManager.GetComponent<ExperimentManager>().practice;

        tempPlayerPosition = player.transform.position;
        tempPlayerRotation = player.transform.rotation.eulerAngles;
        tempSpherePosition = sphere.transform.position;
        tempSphereRotation = sphere.transform.rotation.eulerAngles;
        tempDiffPlayerSphere = player.GetComponent<RotateCamera>().diffPlayerSphere;
        currentState = experimentManager.GetComponent<ExperimentManager>().currentState.ToString();
        tempTime = 0.0f;
        totalTime = 0.0f;
        
        // prep output file
        string subPath = "Data"; 
        bool exists = System.IO.Directory.Exists(subPath); // test if data dir exists

        if (!exists)
            System.IO.Directory.CreateDirectory(subPath);

        // output file name
        myfilename = "Data/"+ sbj + "_" + vision + "_" + explore + "_" + timeStamp + ".csv";

        // open new file
        sw = new StreamWriter(myfilename);

        // write a header to file
        string header = "trial, practice, x, y, z, playerPitch, playerYaw, playerRoll, sphere.x, sphere.y, sphere.z, spherePitch, sphereYaw, sphereRoll, diffSP.x, diffSP.y, diffSP.z,  currentState, trialTime, totalTime";
        sw.WriteLine(header);
    }
	
	// log the data
	void Update () {
        trial = experimentManager.GetComponent<ExperimentManager>().counter;
        practice = experimentManager.GetComponent<ExperimentManager>().practice;

        tempPlayerPosition = player.transform.position;
        tempPlayerRotation = player.transform.rotation.eulerAngles;
        tempSpherePosition = sphere.transform.position;
        tempSphereRotation = sphere.transform.rotation.eulerAngles;
        tempDiffPlayerSphere = player.GetComponent<RotateCamera>().diffPlayerSphere;
        currentState = experimentManager.GetComponent<ExperimentManager>().currentState.ToString();
        tempTime = experimentManager.GetComponent<ExperimentManager>().trialTime; // time passed in each exp state
        totalTime += Time.fixedDeltaTime; // total time passed in experiment
        
        WriteToFile();
    }

    // write position, rotation, events and time stamp to file
    void WriteToFile()
    {
        string output = trial + "," + practice +"," +
                        tempPlayerPosition.x + "," + tempPlayerPosition.y + "," + tempPlayerPosition.z + "," + 
                        tempPlayerRotation.x + "," + tempPlayerRotation.y + "," + tempPlayerRotation.z + "," +
                        tempSpherePosition.x + "," + tempSpherePosition.y + "," + tempSpherePosition.z + "," +
                        tempSphereRotation.x + "," + tempSphereRotation.y + "," + tempSphereRotation.z + "," +
                        tempDiffPlayerSphere.x + "," + tempDiffPlayerSphere.y + "," + tempDiffPlayerSphere.z + "," +
                        currentState +"," + tempTime + "," + totalTime;
        sw.WriteLine(output);
    }

    // close file on quit
    void OnApplicationQuit()
    {
        sw.Close();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
