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
    // Defince current state/experiment state options
    public enum ExperimentState {Idle, ITI, Trial, EndTrial, ShowTargetView, BetweenViews, ShowStartView, SbjResponding, QuitApp};
    public ExperimentState currentState = ExperimentState.Idle;

    // experiment control variables
    public bool practice = true;    // practice (true) or test (false) trials?
    public int  counter = 0;        // counter that increases by 1 at the end of each trial
    public bool targetOnScreen;     // during practice: is target 2 in the users FOV?
    public bool buttonsEnabled;     // toggle user input
    private float startTime;
    public float trialTime;         // timer that's reset at each experiment state

    // Experiment GameObjects
    public GameObject waitForInstructions;  // Text instructions
    public GameObject clicktoStart;         // Text instructions
    private Serializer serializer;          // writes data to file
    private GameObject player;              // main camera
    private GameObject sphere;              // photosphere
    private GameObject target1;             // grey start target in practice trials
    private GameObject target2;             // red target in practice trials

    // bools to toggle what is called during Update
    bool isCoroutineStarted = false;
    bool calledOnceITI = false;
    bool calledOnceTrial = false;
    bool calledOnceEndTrial = false;
    bool calledOnceShowTargetView = false;
    bool calledOnceBetweenViews = false;
    bool calledOnceShowStartView = false;
    bool calledOnceSbjResponding = false;

    // For testing
    bool onTest = false;

    // On Experiment start up
    void Start()
    {
        // find all relevant gameobjects
        player = GameObject.Find("Main Camera");
        sphere = GameObject.Find("Sphere 1");
        target1 = GameObject.Find("Target 1");
        target2 = GameObject.Find("Target 2");
        buttonsEnabled = true;
        
        ResetVars(); //reset variables 
    }

    // Main experiment loop: ExperimentState and practice/!practice determine what happens
    void Update()
    {
        targetOnScreen = player.GetComponent<RotateCamera>().targetOnScreen; // test if red target is in FOV
        ProcessUserInput(); // check for input from participant/experimenter

        // Default state is Idle
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
            switch (currentState)
            {
                case ExperimentState.ITI:
                    if (!calledOnceITI)
                    {
                        startTime = Time.time;
                        // set texture
                        player.GetComponent<RotateCamera>().SetTexture();
                        // fade out
                        player.GetComponent<RotateCamera>().FadeOut();
                        // Rotate sphere to random start position
                        player.GetComponent<RotateSphere>().Rotate();
                        // make target 1 align with camera
                        target1.GetComponent<RotateTarget>().FixTarget();
                        // rotate target 2 to random position
                        target2.GetComponent<RotateTarget>().Rotate();

                        calledOnceITI = true;
                    }
                    trialTime = Time.time - startTime;
                    break;
            case ExperimentState.Trial:
                    if (!calledOnceTrial)
                    {
                        startTime = Time.time;
                        player.GetComponent<RotateCamera>().FadeIn();
                        calledOnceTrial = true;
                    }
                    if (targetOnScreen) {
                        ChangeState(ExperimentState.EndTrial);
                    }
                    trialTime = Time.time - startTime;
                    break;
            case ExperimentState.EndTrial:
                    if (!calledOnceEndTrial)
                    {
                        startTime = Time.time;
                        player.GetComponent<RotateCamera>().FadeOut();
                        calledOnceEndTrial = true;
                    }
                    counter += 1; // increase counter by 1 at end of each trial
                    ResetVars();  // Reset toggle variables

                    // decide what happens at end of trial
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
                    trialTime = Time.time - startTime;
                    break;
            }
        }
        // Loop for test trials
        else if (!practice)
        {                 
            switch (currentState)
            {
                case ExperimentState.ITI:
                    if (!calledOnceITI)
                    {
                        startTime = Time.time;
                        buttonsEnabled = true;
                        player.GetComponent<RotateCamera>().FadeOut();     // fade out   
                        target1.SetActive(false);                        // deactivate the grey target spheres in test trials
                        player.GetComponent<RotateCamera>().SwapShader();  // Switch between wireframe and insideout shader                      
                        player.GetComponent<RotateSphere>().ResetSphere(); // Reset local euler angles of sphere rotation to 0 rand 0 
                        target2.GetComponent<RotateTarget>().Rotate();   // move target to elevation zero and at random orientation from camera view   
                        calledOnceITI = true;
                    }
                    trialTime = Time.time - startTime;
                    break;
                case ExperimentState.Trial:
                    if (!calledOnceTrial)
                    {
                        startTime = Time.time;
                        target2.SetActive(true);
                        player.GetComponent<RotateCamera>().SetTexture(); // set texture 
                        player.GetComponent<RotateCamera>().FadeIn();     // fade in  
                        calledOnceTrial = true;
                    }
                    // toggle instructions when participant is facing in the right directions
                    if (targetOnScreen)
                    {
                        buttonsEnabled = true;
                        clicktoStart.SetActive(true);
                    }
                    else {
                        buttonsEnabled = false;
                        clicktoStart.SetActive(false);
                    }
                    trialTime = Time.time - startTime;
                    break;
                case ExperimentState.ShowTargetView:
                    if (!calledOnceShowTargetView)
                    {
                        startTime = Time.time;
                        buttonsEnabled = false;
                        clicktoStart.SetActive(false);
                        target2.SetActive(false);
                        player.GetComponent<RotateCamera>().SwapShader(); // Switch between wireframe and insideout shader
                        player.GetComponent<RotateCamera>().FadeIn();
                        calledOnceShowTargetView = true;
                    }
                    if (!isCoroutineStarted)
                    {
                        StartCoroutine("TrialCoroutine");
                    }
                    trialTime = Time.time - startTime;
                    break;
                case ExperimentState.BetweenViews:
                    if (!calledOnceBetweenViews)
                    {
                        startTime = Time.time;
                        player.GetComponent<RotateCamera>().FadeOut();
                        player.GetComponent<RotateSphere>().Rotate(); // Rotate sphere to new random view
                        calledOnceBetweenViews = true;
                    }
                    if (!isCoroutineStarted)
                    {
                        StartCoroutine("TrialCoroutine");
                    }
                    trialTime = Time.time - startTime;
                    break;
                case ExperimentState.ShowStartView:
                    if (!calledOnceShowStartView)
                    {
                        startTime = Time.time;
                        player.GetComponent<RotateCamera>().FadeIn(); // FadeIn
                        calledOnceShowStartView = true;
                    }
                    if (!isCoroutineStarted)
                    {
                        StartCoroutine("TrialCoroutine");
                    }
                    trialTime = Time.time - startTime;
                    break;
                case ExperimentState.SbjResponding:
                    if (!calledOnceSbjResponding)
                    {
                        startTime = Time.time;
                        buttonsEnabled = true;
                        player.GetComponent<RotateCamera>().FadeOut(); // FadeOut 
                        calledOnceSbjResponding = true;
                    }
                    trialTime = Time.time - startTime;
                    break;
                case ExperimentState.EndTrial:                    
                    if (!calledOnceEndTrial)
                    {
                        startTime = Time.time;
                        counter += 1; // increase counter 
                        player.GetComponent<RotateCamera>().FadeOut();
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
                    trialTime = Time.time - startTime;
                    break;
            }
        }
    }

    // manages timing in test trials
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

    // Change Experiment state
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

    // define how and when states are switched on user input
    private void ProcessUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && buttonsEnabled)
        {
            switch (currentState)
            {
                case ExperimentState.Idle:
                    clicktoStart.SetActive(true);
                    ChangeState(ExperimentState.ITI);  
                    break;
                case ExperimentState.ITI:
                    clicktoStart.SetActive(false);
                    ChangeState(ExperimentState.Trial); 
                    break;
                case ExperimentState.Trial:
                    ChangeState(ExperimentState.ShowTargetView);
                    break;
                case ExperimentState.SbjResponding:
                    clicktoStart.SetActive(true);
                    ChangeState(ExperimentState.EndTrial); 
                    break;
                default:
                    break;
            }
            // return time and current state for debugging
            Debug.Log("User input at time: " + Time.time + " current state: " + currentState);
        } 
        else if (Input.GetKeyDown(KeyCode.I)) // for testing
        {
            target2.SetActive(true);
            target2.transform.localPosition = new Vector3(0, 0, 0.4f);
            if (!onTest)
            {
                player.GetComponent<RotateCamera>().FadeIn();                
                onTest = true;
                print("FADE IN!");
            }
            else
            {
                player.GetComponent<RotateCamera>().FadeOut();                
                onTest = false;
                print("Fade Out!");
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