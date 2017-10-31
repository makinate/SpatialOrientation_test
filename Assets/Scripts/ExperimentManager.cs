using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Experiment Manager: sets experiment states in Update(); Experiment states are switched
/// by clicks (for Development) or taps (either hand or clicker). 
/// </summary>

public class ExperimentManager : MonoBehaviour
{
    public enum ExperimentState {Idle, WaitToStart, PrepTasks, InstrTask1, BeginTask1, Task1, EndTask1, BeginTask2, Task2, EndTask2, QuitApp };
    public ExperimentState currentState = ExperimentState.Idle;
    
    
    private float startTime;
    private Serializer serializer;


    ///  <summary>
    ///  Switch to a designated state and handle the associated changes to the experimental and stimulus settings
    /// </summary>
    /// <param name = "newState"> The state to switch to</param>
    private void ChangeState(ExperimentState newState)
    {
        // define what happens in each state
        switch (newState)
        {
            case ExperimentState.Idle:
                break;
            case ExperimentState.PrepTasks:
                startTime = Time.time;                      // Define Starttime for state
                
                break;
            case ExperimentState.InstrTask1:
                startTime = Time.time;                      // Define Starttime for state
                //textToSpeechManager.SpeakText("Your first task is to find a chair and sit down as quickly as possible. Use the clicker as soon as you sat down. Click to start when you are ready!");
                
                // ChangeMesh(); // loads an existing room model (high res mesh) and uses that instead of the online mesh. DON'T DO THIS YET.
                break;
            case ExperimentState.BeginTask1:
                startTime = Time.time;                      // Define Starttime for state
                
                break;
            case ExperimentState.Task1:
                startTime = Time.time;                      // Define Starttime for state
                break;
            case ExperimentState.EndTask1:
                startTime = Time.time;                      // Define Starttime for state
                
                break;
            case ExperimentState.BeginTask2:
                
                break;
            case ExperimentState.Task2:
                startTime = Time.time;
                break;
            case ExperimentState.EndTask2:
                startTime = Time.time;
                
                break;
            case ExperimentState.QuitApp:
                //startTime = Time.time;                      // Define Starttime for state
                Application.Quit(); // Quit App
                break;
            default:
                break;

        }
        currentState = newState;
    }

    private void Awake()
    {
        
    }

    void Update()
    {

        ProcessUserInput();

        // loop through experiment states and change state if time dependent event
        switch (currentState)
        {
            case ExperimentState.Idle:
                //Debug.Log("Idle");
                break;
            case ExperimentState.BeginTask1:
                if (Time.time - startTime >= 1) // Go to trial
                {
                    ChangeState(ExperimentState.Task1);
                }
                break;
            case ExperimentState.EndTask1:
                if (Time.time - startTime >= 10) // Go to next phase after a TWO MINUTES
                {
                    ChangeState(ExperimentState.QuitApp);
                }
                break;
            case ExperimentState.BeginTask2:
                if (Time.time - startTime >= 1) // Go to trial
                {
                    ChangeState(ExperimentState.Task2);
                }
                break;
            case ExperimentState.Task2:
                if (Time.time - startTime >= 30) // Go to next phase after a TWO MINUTES
                {
                    ChangeState(ExperimentState.EndTask2);
                }
                break;
            case ExperimentState.EndTask2:
                if (Time.time - startTime >= 10) // Go to next phase after a TWO MINUTES
                {
                    ChangeState(ExperimentState.QuitApp);
                }
                break;
            default:
                break;
        }
    }

    // define how and when states are switched on click or tab change experiment state
    // possible options are 1) UserInput (tap or click) 2) on Timer 3) tapCount
    private void ProcessUserInput()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
        {
            
            switch (currentState)
            {
                case ExperimentState.Idle:
                    ChangeState(ExperimentState.WaitToStart);  // Change Experiment State to next 
                    
                    break;
                case ExperimentState.WaitToStart:
                    ChangeState(ExperimentState.PrepTasks);
                    
                    break;
                case ExperimentState.PrepTasks:
                    ChangeState(ExperimentState.InstrTask1);
                    
                    break;
                case ExperimentState.InstrTask1:
                    ChangeState(ExperimentState.BeginTask1);
                    
                    break;
                case ExperimentState.BeginTask1:
                    //ChangeState(ExperimentState.Task1);
                    
                    break;
                case ExperimentState.Task1:
                    ChangeState(ExperimentState.EndTask1);
                    
                    break;
                case ExperimentState.EndTask1:
                    //ChangeState(ExperimentState.BeginTask2);
                    
                    break;
                case ExperimentState.BeginTask2:
                    
                    break;
                default:
                    break;
            }
            // return time and current state for debugging
            Debug.Log("Time: " + Time.time + " current state: " + currentState);
        }

    }

    
}