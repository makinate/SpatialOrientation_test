using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Control events in practice trials
/// </summary>
public class TestTrials : MonoBehaviour
{


    private GameObject em;
    private string currentState;
    bool practice;
    public GameObject player;
    private GameObject sphere;

    public bool calledOnceITI = false;
    public bool calledOnceTrial = false;
    public bool calledOnceEndTrial = false;
    public bool calledOnceShowTargetView = false;
    public bool calledOnceBetweenViews = false;
    public bool calledOnceShowStartView = false;
    public bool calledOnceSbjResponding = false;


    void Start()
    {

        // find all relevant gameobjects
        em     = GameObject.Find("ExperimentManager");
        player = GameObject.Find("Main Camera");
        sphere = GameObject.Find("Sphere 1");
        //reset variables 
        ResetVars();
    }

    void Update()
    {

        // figure out experiment state and if this is a practice trial
        currentState = em.GetComponent<ExperimentManager>().currentState.ToString();
        practice = em.GetComponent<ExperimentManager>().practice;

        if (!practice)
        {
            if (currentState == "ITI")
            {
                if (!calledOnceITI)
                {
                    // set texture
                    player.GetComponent<MoveCamera>().SetTexture();
                    // fade out
                    player.GetComponent<MoveCamera>().fadeOut();
                    // Rotate sphere to random start position
                    player.GetComponent<MoveCamera>().RotateSphere();
                    calledOnceITI = true;
                }
            }
            else if (currentState == "Trial")
            {
                if (!calledOnceTrial)
                {
                    // fade in
                    player.GetComponent<MoveCamera>().fadeIn();
                    calledOnceTrial = true;
                }
            }
            else if (currentState == "ShowTargetView")
            {
                if (!calledOnceShowTargetView)
                {
                    // fade in
                    player.GetComponent<MoveCamera>().fadeIn();
                    calledOnceShowTargetView = true;
                }
            }
            else if (currentState == "BetweenViews")
            {
                if (!calledOnceBetweenViews)
                {
                    // fadeout 
                    player.GetComponent<MoveCamera>().fadeOut();
                    // Rotate sphere to next view
                    player.GetComponent<MoveCamera>().RotateSphere();
                    calledOnceBetweenViews = true;
                }
            }
            else if (currentState == "ShowStartView")
            {
                if (!calledOnceShowStartView)
                {
                    // fadeout 
                    player.GetComponent<MoveCamera>().fadeIn();
                    calledOnceShowStartView = true;
                }
            }
            else if (currentState == "SbjResponding")
            {
                if (!calledOnceSbjResponding)
                {
                    // fadeout 
                    player.GetComponent<MoveCamera>().fadeOut();
                    calledOnceSbjResponding = true;
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
    }
    
    
    // reset "called once" toggle variables
    void ResetVars()
    {
        calledOnceITI = false;
        calledOnceTrial = false;
        calledOnceEndTrial = false;
        calledOnceShowTargetView = false;
        calledOnceBetweenViews = false;
        calledOnceShowStartView = false;
        calledOnceSbjResponding = false;
    }

}
