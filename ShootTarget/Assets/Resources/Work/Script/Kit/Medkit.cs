using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int addHealth = 50;
    public int score = 300;

    PlayerHealth playerHealth;
    ScoreManager scoreManager;
    Animator anim;
    public GameObject frame;
    public GameObject medKit;

    // Start is called before the first frame update
    void Start()
    {
        frame.gameObject.SetActive(true);
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
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

    public void HitMedKit()
    {
        FloatingTextController.CreateFloatingText(score.ToString(), transform);

        playerHealth.AddHealth(addHealth);
        scoreManager.GetScore(score);
        Destroy(medKit);
    }
}
