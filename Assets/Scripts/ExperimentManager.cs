using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Experiment Manager: sets experiment states in Update(); Experiment states are switched
/// by space bar, clicking, head movement or timers. Different Trial structure depending in practice and test trials. 
/// </summary>

public class ExperimentManager : MonoBehaviour
{
    public enum ExperimentState {Idle, ITI, Trial, EndTrial, ShowTargetView, BetweenViews, ShowStartView, SbjResponding, QuitApp};
    public ExperimentState currentState = ExperimentState.Idle;
    public bool practice = true;
    public int counter = 0;
    public bool targetOnScreen;
    public GameObject waitForInstructions;
    public GameObject clicktoStart;

    private float startTime;
    private Serializer serializer;
    private GameObject player;
    private GameObject sphere;
    private GameObject target1;
    private GameObject target2;

    public bool isCoroutineStarted = false;
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
        player = GameObject.Find("Main Camera");
        sphere = GameObject.Find("Sphere 1");
        target1 = GameObject.Find("Target 1");
        target2 = GameObject.Find("Target 2");
        //reset variables 
        ResetVars();
    }
 
    // Main experiment loop: ExperimentState and practice/!practice determine what happens
    void Update()
    {
        // check for input from participant
        ProcessUserInput();

        switch (currentState)
        {
            case ExperimentState.Idle:
                waitForInstructions.SetActive(true);
                break;
            default:
                waitForInstructions.SetActive(false);
                break;
        }

        // Loop for practice trials
        if (practice)
        {
            targetOnScreen = player.GetComponent<MoveCamera>().targetOnScreen;

            switch (currentState)
            {
                case ExperimentState.ITI:
                    if (!calledOnceITI)
                    {
                        // set texture
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
                    break;
            case ExperimentState.Trial:
                    if (!calledOnceTrial)
                    {
                        // fade into practice trial
                        player.GetComponent<MoveCamera>().fadeIn();
                        calledOnceTrial = true;
                    }
                    if (targetOnScreen) {
                        ChangeState(ExperimentState.EndTrial);
                    }
                break;
            case ExperimentState.EndTrial:
                    counter = counter + 1;
                    
                    if (!calledOnceEndTrial)
                    {
                        player.GetComponent<MoveCamera>().fadeOut();
                        calledOnceEndTrial = true;
                    }
                    ResetVars();  

                    if (counter == 5)
                    {
                        Debug.Log("PRACTICE DONE");
                        practice = false;
                        counter = 0;
                        ChangeState(ExperimentState.Idle);


                    }
                    else
                    {
                        clicktoStart.SetActive(true);
                        ChangeState(ExperimentState.ITI);
                    }
                    
                    break;

            }
        }
        // Loop for test trials
        else if (!practice)
        {
            // deactivate the red and grey target spheres if not training
            target1.SetActive(false);
            target2.SetActive(false);

            switch (currentState)
            {

                case ExperimentState.ITI:
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
                    break;
                case ExperimentState.Trial:
                    if (!calledOnceTrial)
                    {
                        // fade in
                        player.GetComponent<MoveCamera>().fadeIn();
                        calledOnceTrial = true;
                    }
                    ChangeState(ExperimentState.ShowTargetView);
                    break;
                case ExperimentState.ShowTargetView:
                    if (!calledOnceShowTargetView)
                    {
                        // fade in
                        player.GetComponent<MoveCamera>().fadeIn();
                        calledOnceShowTargetView = true;
                    }
                    if (!isCoroutineStarted)
                    {
                        StartCoroutine("TrialCoroutine");
                    }
                    break;
                case ExperimentState.BetweenViews:
                    if (!calledOnceBetweenViews)
                    {
                        // fadeout 
                        player.GetComponent<MoveCamera>().fadeOut();
                        player.GetComponent<MoveCamera>().RotateSphere();
                        calledOnceBetweenViews = true;
                    }
                    if (!isCoroutineStarted)
                    {
                        StartCoroutine("TrialCoroutine");
                    }
                    break;
                case ExperimentState.ShowStartView:
                    if (!calledOnceShowStartView)
                    {
                        // fadeout 
                        player.GetComponent<MoveCamera>().fadeIn();
                        calledOnceShowStartView = true;
                    }
                    if (!isCoroutineStarted)
                    {
                        StartCoroutine("TrialCoroutine");
                    }
                    break;
                case ExperimentState.SbjResponding:
                    if (!calledOnceSbjResponding)
                    {
                        // fadeout 
                        player.GetComponent<MoveCamera>().fadeOut();
                        calledOnceSbjResponding = true;
                    }

                    break;
                case ExperimentState.EndTrial:
                    
                    if (!calledOnceEndTrial)
                    {
                        counter = counter + 1;
                        player.GetComponent<MoveCamera>().fadeOut();

                        calledOnceEndTrial = true;
                    }
                    ResetVars();

                    if (counter == 5)
                    {
                        Debug.Log("EXPERIMENT DONE");
                        ChangeState(ExperimentState.QuitApp);
                        practice = false;
                        counter = 0;
                    }
                    else
                    {
                        clicktoStart.SetActive(true);
                        ChangeState(ExperimentState.ITI);
                    }
                    break;

            }
        }
    }
    IEnumerator TrialCoroutine()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(0.5f); // waits 0.5 seconds
        isCoroutineStarted = false;
        switch (currentState)
        {
            
            case ExperimentState.ShowTargetView:
                ChangeState(ExperimentState.BetweenViews);
                break;
            case ExperimentState.BetweenViews:
                ChangeState(ExperimentState.ShowStartView);
                break;
            case ExperimentState.ShowStartView:
                ChangeState(ExperimentState.SbjResponding);
                break;
        }
        

    }
    private void ChangeState(ExperimentState newState)
    {
        // define what happens in each state
        switch (newState)
        {
            case ExperimentState.QuitApp:
                Application.Quit(); // Quit App
                break;
            default:
                break;

        }
        currentState = newState;
    }
    // define how and when states are switched on click or tab change experiment state
    // possible options are 1) UserInput (tap or click) 2) on Timer 3) tapCount
    private void ProcessUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case ExperimentState.Idle:
                    clicktoStart.SetActive(true);
                    ChangeState(ExperimentState.ITI);  // Change Experiment State to next 

                    break;
                case ExperimentState.ITI:

                    clicktoStart.SetActive(false);
                    ChangeState(ExperimentState.Trial);  // Change Experiment State to next 

                    break;
                case ExperimentState.SbjResponding:

                    clicktoStart.SetActive(true);
                    ChangeState(ExperimentState.EndTrial);  // Change Experiment State to next 

                    break;
                default:
                    break;
            }
            // return time and current state for debugging
            Debug.Log("Time: " + Time.time + " current state: " + currentState);
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