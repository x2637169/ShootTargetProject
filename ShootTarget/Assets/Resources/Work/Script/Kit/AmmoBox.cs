using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int score = 300;
    public int addAmmo;

    public Ammo ammo;
    Animator anim;
    ScoreManager scoreManager;
    public GameObject frame;
    public GameObject ammoBox;

    // Start is called before the first frame update
    void Start()
    {
        frame.gameObject.SetActive(true);
        ammo = GameObject.FindWithTag("Player").GetComponentInChildren<Ammo>();
        scoreManager = GameObject.FindWithTag("Manager").GetComponent<ScoreManager>();
        anim = GetComponent<Animator>();
        anim.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            anim.enabled = false;
            frame.gameObject.SetActive(false);
        }
    }

    public void HitAmmoBox()
    {
        FloatingTextController.CreateFloatingText(score.ToString(), transform);

        ammo.AddAmmo(addAmmo);
        scoreManager.GetScore(score);
        Destroy(ammoBox);
    }
}
