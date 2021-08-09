using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunKit : MonoBehaviour
{
    public int score = 300;
    public float haveGunTime;

    Gun gun;
    ScoreManager scoreManager;
    Animator anim;
    public GameObject frame;
    public GameObject gunKit;

    // Start is called before the first frame update
    void Start()
    {
        frame.gameObject.SetActive(true);
        gun = GameObject.FindWithTag("Player").GetComponentInChildren<Gun>();
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

    public void HitGunKit()
    {
        gun.SwitchGun(haveGunTime);
        FloatingTextController.CreateFloatingText(score.ToString(), transform);
        Destroy(gunKit);
    }
}
