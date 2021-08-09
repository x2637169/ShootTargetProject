using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Animator animPopup;
    private Text damageText;
    // Start is called before the first frame update
    void OnEnable()
    {
        animPopup.updateMode = AnimatorUpdateMode.UnscaledTime;
        AnimatorClipInfo[] clipInfo = animPopup.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animPopup.GetComponent<Text>();
    }

    public void SetText(string text)
    {
        damageText.text = text; 
    }
}