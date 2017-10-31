using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentUI : MonoBehaviour
{
    public GameObject GO;

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
    }
}