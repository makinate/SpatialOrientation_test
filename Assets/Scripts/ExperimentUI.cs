using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// for testing purposes: toggle GUI and data recording
public class ExperimentUI : MonoBehaviour
{
    public GameObject GO;
    bool on = false;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Enable"))
        {
            Debug.Log("Enable: " + GO.name);
            GO.SetActive(true);
        }
        if (GUI.Button(new Rect(10, 50, 100, 30), "Disable"))
        {
            Debug.Log("Disable: " + GO.name);
            GO.SetActive(false);
            
        }
        if (GUI.Button(new Rect(10, 90, 100, 30), "Record"))
        {
            
            if (on) { 
                gameObject.GetComponent<Serializer>().enabled = false;
                on = false;
            }
            else
            {
                gameObject.GetComponent<Serializer>().enabled = true;
                on = true;
            }
        }
    }
}