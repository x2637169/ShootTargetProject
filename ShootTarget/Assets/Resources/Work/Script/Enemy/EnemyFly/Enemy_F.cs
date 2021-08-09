using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_F : MonoBehaviour
{
    Animator anim;
    
    public float timeCanShoot;
    private float canShoot;
    public bool CanShoot;

    EnemyHealth_F enemyHealth_F;
    EnemyAttack_F enemyAttack_F;
    Collider enemyCollider;
    TimeManager timeManager;

    Collider colliderItem;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyHealth_F = GetComponent<EnemyHealth_F>();
        enemyCollider = GetComponent<Collider>();
        enemyAttack_F = GetComponent<EnemyAttack_F>();
        timeManager = GameObject.FindWithTag("Manager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();

        canShoot += Time.deltaTime;

        if (timeCanShoot < canShoot)
        {
            CanShoot = true;
        }
    }

    public void ChildCollidersOn()
    {
        var collidersObj = gameObject.GetComponentsInChildren<Collider>();
        for (var i = 0; i < collidersObj.Length; i++)
        {
            colliderItem = collidersObj[i];
            colliderItem.enabled = true;
        }
    }

    public void ChildCollidersOff()
    {
        var collidersObj = gameObject.GetComponentsInChildren<Collider>();
        for (var i = 0; i < collidersObj.Length; i++)
        {
            colliderItem = collidersObj[i];
            colliderItem.enabled = false;
        }
    }

    void Animation()
    {
        anim.SetBool("Idle", true);
    }

    public void StopAnimation()
    {
        anim.SetBool("Idle", false);
        ChildCollidersOff();
    }
}