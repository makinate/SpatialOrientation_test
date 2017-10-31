using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public GameObject player;
    public Transform sphere1;
    public Transform sphere2;
    public Transform sphere3;
    public Transform sphere4;
    public Transform sphere5;
    public Transform sphere6;
    public Transform sphere7;
    public Transform sphere8;
    

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            player.transform.position = sphere1.transform.position;
            //Debug.Log("Position 1");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            player.transform.position = sphere2.transform.position;
            //Debug.Log("Position 2");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            player.transform.position = sphere3.transform.position;
            //Debug.Log("Position 3");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            player.transform.position = sphere4.transform.position;
            //Debug.Log("Position 4");
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            player.transform.position = sphere5.transform.position;
            //Debug.Log("Position 5");
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            player.transform.position = sphere6.transform.position;
            //Debug.Log("Position 6");
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            player.transform.position = sphere7.transform.position;
            //Debug.Log("Position 7");
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            player.transform.position = sphere8.transform.position;
            //Debug.Log("Position 8");
        }
    }
}
