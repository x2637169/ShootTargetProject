using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    Animator anim_Enemy_Walk;
    AudioSource enemy_Walk_Aduio;
    public AudioClip enemyShootSound;

    Enemy enemy;
    public GameObject player;

    public GameObject muzzle;

    public int damage = 10;
    public float speed;
    public float nextFireTime;
    public float fireDeltaTime;
    public float minNextFireTime;
    public float maxNextFireTime;
    public float disEffectTime;

    public bool IsShooting;
    public bool CanShoot;

    Enemy_Health enemy_Health;

    // Start is called before the first frame update
    void Start()
    {
        anim_Enemy_Walk = GetComponent<Animator>();
        enemy_Walk_Aduio = GetComponent<AudioSource>();
        enemy = GetComponent<Enemy>();
        enemy_Health = GetComponent<Enemy_Health>();
        player = GameObject.FindWithTag("Player");

        muzzle.SetActive(false);

        nextFireTime = Random.Range(minNextFireTime, maxNextFireTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_Health.dead) return;

        LookPlayer();

        if (!CanShoot) return;
        fireDeltaTime += Time.deltaTime;
        if (nextFireTime <= fireDeltaTime)
        {
            fireDeltaTime = 0;
            Attack();
        }
    }

    void LookPlayer()
    {
        if (!enemy_Health.dead)
        {
            Vector3 playerPosition = player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(playerPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    void Attack()
    {
        IsShooting = true;

        StartCoroutine(gunEffect());

        anim_Enemy_Walk.SetTrigger("Shoot");

        nextFireTime = Random.Range(minNextFireTime, maxNextFireTime);

        enemy_Walk_Aduio.PlayOneShot(enemyShootSound, 0.8f);

        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.PlayerTakeDamage(damage);
        }
    }

    IEnumerator gunEffect()
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(disEffectTime);
        muzzle.SetActive(false);
        anim_Enemy_Walk.SetTrigger("Idle");
        IsShooting = false;
    }

    public void ClearEffect()
    {
        muzzle.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanShoot) return;

        if (other.tag == "EnemyShootArea")
        {
            CanShoot = true;
        }
    }
}