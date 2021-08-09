using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoAnimation : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AmmoOutAnim()
    {
        anim.SetTrigger("AmmoOut");
    }

    public void SmgAmmoFade()
    {
        anim.SetBool("AmmoFade", true);
    }

    public void SmgAmmoDisFade()
    {
        anim.SetBool("AmmoFade", false);
    }

    void InActiveAmmo()
    {
        gameObject.SetActive(false);
    }
}
