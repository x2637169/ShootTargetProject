using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{

    /* public CanvasGroup uiElement;

    public float startUiElementValue;
    public float endLerpTime;
    public float currentUiElementValue;

    public float uiElementTime;
    public float uiElementDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        currentUiElementValue = startUiElementValue;
    }

    // Update is called once per frame
    void Update()
    {
        uiElement.alpha = currentUiElementValue;
        currentUiElementValue = Mathf.Clamp(currentUiElementValue, 0, 1);

        uiElementDeltaTime += Time.unscaledDeltaTime;
        if (uiElementDeltaTime >= uiElementTime)
            currentUiElementValue -= endLerpTime * Time.unscaledDeltaTime;

        if (currentUiElementValue < 0)
            Destroy(gameObject);
    }*/

    void Destroy()
    {
        Destroy(gameObject);
    }
}
