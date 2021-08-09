using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CinematicEffects;

public class SlowMotionPower : MonoBehaviour
{
    public SlowMotionAnimation slowMotionAnimation;
    public float slowTimeScale;
    public float normalTimeScale;

    public float slowTime;
    [HideInInspector] public float slowDeltaTime;
    public float exSlowTime;
    [HideInInspector] public float exSlowDeltaTime;

    public int slowMotionMaxCount;
    [HideInInspector] public int slowMotionCount;

    [HideInInspector] public bool SlowMotionOn;
    [HideInInspector] public bool ExSlowMotion;
    public bool callSlowMotion;

    public int slowMotionHeadChance;
    public int slowMotionBodyChance;

    AudioSource slowMotionAudio;
    public AudioClip slowMotionSoundFront;
    public AudioClip slowMotionSoundBack;

    void Awake()
    {
        slowMotionAudio = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {
        //呼叫慢動作
        if (callSlowMotion && slowMotionCount <= slowMotionMaxCount)
        {
            if (SlowMotionOn)
                ExSlowMotionActive();
            else
                SlowMotionActive();
        }

        if (SlowMotionOn)
        {
            if (ExSlowMotion)
            {
                exSlowDeltaTime += Time.deltaTime;
                if (exSlowDeltaTime >= exSlowTime)
                    SlowMotionOff();
            }
            else
            {
                slowDeltaTime += Time.deltaTime;
                if (slowDeltaTime >= slowTime)
                    SlowMotionOff();
            }
        }
    }

    public void SlowMotionChance(int slowMotionSelectionc)
    {
        if (slowMotionCount >= slowMotionMaxCount) return;

        //啟動慢動作後增加慢動作時間
        if (SlowMotionOn)
        {
            ExSlowMotionActive();
        }
        else
        {
            //根據部位不同機率啟動慢動作
            int randomChance = Random.Range(0, 100);

            if (slowMotionSelectionc == 1 && randomChance < slowMotionHeadChance)
                SlowMotionActive();
            else if (slowMotionSelectionc == 2 && randomChance < slowMotionBodyChance)
                SlowMotionActive();
        }
    }

    public void SlowMotionActive()
    {
        slowMotionAnimation.FadeIn();
    
        callSlowMotion = false;
        SlowMotionOn = true;

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        slowMotionAudio.PlayOneShot(slowMotionSoundFront);
    }

    public void ExSlowMotionActive()
    {
        if (slowMotionCount >= slowMotionMaxCount) return;

        callSlowMotion = false;
        ExSlowMotion = true;

        slowMotionCount += 1;
        exSlowDeltaTime = 0;

        slowMotionAudio.PlayOneShot(slowMotionSoundFront);
    }

    public void SlowMotionOff()
    {
        slowMotionAnimation.FadeOut();

        slowMotionCount = 0;
        slowDeltaTime = 0;
        exSlowDeltaTime = 0;

        SlowMotionOn = false;
        ExSlowMotion = false;

        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        slowMotionAudio.PlayOneShot(slowMotionSoundBack);
    }
}
