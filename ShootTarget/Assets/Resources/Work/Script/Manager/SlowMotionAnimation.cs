using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionAnimation : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    /* public CanvasGroup uiElement;

    public float startLerpTime;
    public float endLerpTime;
    private float currentValue;

    SlowMotionPower slowMotionPower;

    void Awake()
    {
        slowMotionPower = GetComponent<SlowMotionPower>();
    }

    void Start()
    {

    }

    void Update()
    {
        uiElement.alpha = currentValue;
        currentValue = Mathf.Clamp(currentValue, 0, 1);

        if (slowMotionPower.SlowMotionOn && currentValue < 1)
        {
            FadeIn();
        }
        else if (!slowMotionPower.SlowMotionOn && currentValue > 0)
        {
            FadeOut();
        }
    }*/

    public void FadeIn()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("FadeIn");
        //currentValue += startLerpTime * Time.unscaledDeltaTime;
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
        //currentValue -= endLerpTime * Time.unscaledDeltaTime;
    }

    void InActive()
    {
        gameObject.SetActive(false);
    }
}
