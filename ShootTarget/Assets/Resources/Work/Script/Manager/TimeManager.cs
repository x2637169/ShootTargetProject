using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    Menu menu;
    SlowMotionPower slowMotionPower;
    public Text timerText;
    public Animator Time_m;
    public AudioSource timerAudio;
    public AudioClip timeRunnigSound;

    [HideInInspector] public float startTime;
    public float minutesTime;
    public float secondsTime;

    public float totalTime;
    public float timeRunningFloat;

    [HideInInspector] public bool timeUp = false;
    [HideInInspector] public bool TimeRunnigSoundBool;

    void Awake()
    {
        menu = GetComponent<Menu>();
        slowMotionPower = GameObject.FindWithTag("SlowMotionManager").GetComponent<SlowMotionPower>();

        startTime = (minutesTime * 60) + secondsTime;
        totalTime = startTime;
    }

    void Start()
    {

    }

    void Update()
    {
        if (menu.Win || menu.Dead || menu.Stop)
            TimeRunnigSound();

        if (Time.timeScale == 0) return;
        if (timeUp) return;

        Timer();
    }

    void Timer()
    {
        totalTime = startTime -= Time.deltaTime;

        if (totalTime < 0)
        {
            TimeUp();

            totalTime = 0;
        }

        if (totalTime < timeRunningFloat)
        {
            TimeRunnig();
        }

        string minutes = ((int)totalTime / 60).ToString("f0");
        string seconds = ((int)totalTime % 60).ToString("f0");

        timerText.text = "Time:" + minutes + ":" + seconds;
    }

    void TimeRunnig()
    {
        if (timeRunningFloat >= totalTime)
        {
            timeRunningFloat -= 1;
            Time_m.SetTrigger("TimeAnimation");
        }

        TimeRunnigSound();
    }

    void TimeRunnigSound()
    {
        if (menu.Win || menu.Dead)
            timerAudio.Stop();

        if (slowMotionPower.SlowMotionOn || menu.Stop)
        {
            timerAudio.pitch = 0;
        }
        else
        {
            timerAudio.pitch = 1;
        }

        if (TimeRunnigSoundBool) return;
        TimeRunnigSoundBool = true;
        timerAudio.clip = timeRunnigSound;
        timerAudio.Play();
    }

    void TimeUp()
    {
        timeUp = true;

        menu.WinMenu();
    }
}
