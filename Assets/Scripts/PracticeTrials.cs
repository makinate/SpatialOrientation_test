using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTrials : MonoBehaviour {

    
    private GameObject experimentManager;
    private string currentState;
    bool practice;
    private GameObject player;
    private GameObject sphere;
    private GameObject target1;
    private GameObject target2;

    bool calledOnceITI = false;
    bool calledOncePracticeTrial = false;
    bool calledOnceEndPracticeTrial = false;


    // Use this for initialization
    void Start () {
        experimentManager = GameObject.Find("ExperimentManager");
        player = GameObject.Find("Main Camera");
        sphere = GameObject.Find("Sphere 1");
        target1 = GameObject.Find("Target 1");
        target2 = GameObject.Find("Target 2");
        player.GetComponent<moveCamera>().SetTexture();
        ResetVars();
    }
	


	// Update is called once per frame
	void Update () {
        currentState = experimentManager.GetComponent<ExperimentManager>().currentState.ToString();
        practice     = experimentManager.GetComponent<ExperimentManager>().practice;

        if (practice) { 
            if (currentState == "ITI")
            {
                

                if (!calledOnceITI) {
                    player.GetComponent<cameraRotator>().fadeOut();
                    player.GetComponent<moveCamera>().RotateSphere();
                    target1.GetComponent<RotateTarget>().FixTarget();
                    target2.GetComponent<RotateTarget>().Rotate();

                    calledOnceITI = true;
                }
            
            }
            else if (currentState == "PracticeTrial")
            {
                

                if (!calledOncePracticeTrial)
                {
                    player.GetComponent<cameraRotator>().fadeIn();
                    calledOncePracticeTrial = true;
                }

            }
            else if (currentState == "EndPracticeTrial")
            {
                

                if (!calledOnceEndPracticeTrial)
                {
                    player.GetComponent<cameraRotator>().fadeOut();
                    calledOnceEndPracticeTrial = true;
                }
                ResetVars();
            }
        }
    }
    // reset "called once" toggle variables
    void ResetVars()
    {
        calledOnceITI = false;
        calledOncePracticeTrial = false;
        calledOnceEndPracticeTrial = false;
    }
}
