using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameTimer : MonoBehaviour
{
    //text that will display the timer
    [SerializeField]
    public Text TimeDisplay;
        
    //how long the level will be played (in seconds)
    [SerializeField]
    public float LevelTimeInSeconds;

    //the in-game time formatted to be used as a string
    private TimeSpan PlayingTime;

    //time that has passed since timer has started
    private float ElapsedTime;

    //state of the timer
    private bool TimerActive;


    //Starts running once the object to which this script is attached is enabled
    private void Start()
    {
        //Zeros timer and deactivates it
        TimeDisplay.text = "00:00.00";
        TimerActive = false;

        //For now, the timer is started as soon as the object is enabled
        StartTimer();
    }

    //Function that activates timer and calls the coroutine when called
    public void StartTimer()
    {
        TimerActive = true;
        StartCoroutine(UpdateTimer());
    }

    //Function that deactivates the timer when called
    public void EndTimer()
    {
        TimerActive = false;
    }

    //Coroutine that updates the time
    IEnumerator UpdateTimer()
    {
        //Variable that records the time at the start of the coroutine
        float startTime = Time.time;

        //Zeros elapsed time 
        ElapsedTime = 0;

        //If the timer is active, the remaining time in the level will be updated
        while (TimerActive == true)
        {
            //Elapsed time is calculated by subtracting the starting time from total passed time
            ElapsedTime = Time.time - startTime;

            //The remaining time is calculated and then formatted for string compatibility
            PlayingTime = TimeSpan.FromSeconds(LevelTimeInSeconds - ElapsedTime);

            //Variable for the displayed timer
            //Key: mm = minutes, ss = seconds, ff = fractions of seconds
            string time = PlayingTime.ToString("mm':'ss'.'ff");
            TimeDisplay.text = time;

            //Note: the timer kept changing too faster than expected without clear cause,
            //so the line below is used to stabilize it
            yield return null;

            //If the timer reaches zero, the timer will be deactivated and the end screen will be called
            if (time == "00:00.00")
            {
                EndTimer();
                yield return new WaitForSeconds(2);
                GameStateManager.EndGame();
            }
        }
    }
}
