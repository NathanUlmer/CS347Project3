using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public Text timecounter;
    private static TimeSpan timePlaying;
    private bool timeGoing;
    public float elapsedTime;
    public string timePlayingstr;
    public float maxTime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timecounter.text = "Time 00:00.00";
        if (timePlaying != null)
        {
            timePlayingstr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timecounter.text = timePlayingstr;
        }
    }

    public void BeginTime()
    {
        timeGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTime());
    }

    public void EndTime()
    {
        timeGoing = false;
    }

    private IEnumerator UpdateTime()
    {
        while (timeGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingstr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timecounter.text = timePlayingstr;

            yield return null;
        }
    }
}
