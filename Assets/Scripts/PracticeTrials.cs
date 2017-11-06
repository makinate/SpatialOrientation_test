using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Control events in practice trials
/// </summary>
public class PracticeTrials : MonoBehaviour {

    
    private GameObject em;
    private string currentState;
    bool practice;
    public GameObject player;
    private GameObject sphere;
    private GameObject target1;
    private GameObject target2;

    bool calledOnceITI = false;
    bool calledOnceTrial = false;
    bool calledOnceEndTrial = false;


    void Start () {

        // find all relevant gameobjects
        em      = GameObject.Find("ExperimentManager");
        player  = GameObject.Find("Main Camera");
        sphere  = GameObject.Find("Sphere 1");
        target1 = GameObject.Find("Target 1");
        target2 = GameObject.Find("Target 2");
        //reset variables 
        ResetVars();
    }

	void Update () {

        // figure out experiment state and if this is a practice trial
        currentState = em.GetComponent<ExperimentManager>().currentState.ToString();
        practice     = em.GetComponent<ExperimentManager>().practice;

        if (practice) {
            if (currentState == "ITI")
            {                
                if (!calledOnceITI) {
                    // set starfield texture
                    player.GetComponent<MoveCamera>().SetTexture();
                    // fade out
                    player.GetComponent<MoveCamera>().fadeOut();
                    // Rotate sphere to random start position
                    player.GetComponent<MoveCamera>().RotateSphere();
                    // make target 1 align with camera
                    target1.GetComponent<RotateTarget>().FixTarget();
                    // rotate target 2 to random position
                    target2.GetComponent<RotateTarget>().Rotate();

                    calledOnceITI = true;
                }            
            }
            else if (currentState == "Trial")
            {
                if (!calledOnceTrial)
                {
                    // fade into practice trial
                    player.GetComponent<MoveCamera>().fadeIn();
                    calledOnceTrial = true;
                }
            }
            else if (currentState == "EndTrial")
            {
                

                if (!calledOnceEndTrial)
                {
                    player.GetComponent<MoveCamera>().fadeOut();
                    calledOnceEndTrial = true;
                }
                ResetVars();
            }
        }
        else
        {
            // deactivate the red and grey target spheres if not training
            target1.SetActive(false);
            target2.SetActive(false);
        }
    }

    // reset "called once" toggle variables
    void ResetVars()
    {
        calledOnceITI = false;
        calledOnceTrial = false;
        calledOnceEndTrial = false;
    }
    
}
