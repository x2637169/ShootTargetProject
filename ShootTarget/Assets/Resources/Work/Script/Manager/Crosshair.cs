using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Crosshair : MonoBehaviour
{
    public Image crossHair;
    public Vector3 origPos;
    public bool CallReturn;

    public float origPosTime = 0;

    void Awake()
    {

    }

    void Start()
    {
        origPos = crossHair.transform.position;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        MobileInput();
#else
        OtherInput();
#endif
    }


    void MobileInput()
    {
        if (Input.touchCount <= 0) return;

        if (Input.touchCount == 1)
        {
            //開始觸碰
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                CallReturn = false;
                crossHair.transform.position = Input.touches[0].position;
            }

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                crossHair.transform.position = Input.touches[0].position;
            }

            //手指離開螢幕
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                CallReturn = true;
                StartCoroutine(ReturnPosition());
            }
        }
    }

    void OtherInput()
    {
        crossHair.transform.position = Input.mousePosition;
    }

    IEnumerator ReturnPosition()
    {
        origPosTime = 0;

        while (origPosTime < 1)
        {
            if (CallReturn)
            {
                origPosTime += Time.unscaledDeltaTime;
                yield return null;
            }
            else
            {
                yield break;
            }
        }

        crossHair.transform.position = origPos;
    }
}
