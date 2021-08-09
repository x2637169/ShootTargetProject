using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_F : MonoBehaviour
{
    Animator anim;
    RaycastHit hit;
    AudioSource enemyAduio;
    public AudioClip enemyShootSound;
    Enemy_F enemy_F;
    EnemyHealth_F enemyHealth_F;
    public GameObject player;
    public Transform Gun;
    public GameObject enemyGun;
    //public LineRenderer gunLine;
    public GameObject muzzle;

    public int damage = 10;
    public float range = 200;
    public float speed;
    public float nextFireTime;
    public float minNextFireTime;
    public float maxNextFireTime;
    public float fireTime;
    public float disEffectTime;
    public int count;
    public float brustTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyAduio = GetComponent<AudioSource>();
        enemy_F = GetComponent<Enemy_F>();
        enemyHealth_F = GetComponent<EnemyHealth_F>();
        player = GameObject.FindWithTag("Player");

        muzzle.SetActive(false);
        //gunLine.enabled = false;

        nextFireTime = Random.Range(minNextFireTime, maxNextFireTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth_F.dead)
        {
            ClearEffect();
        }

        if (!enemy_F.CanShoot) return;
        if (enemyHealth_F.dead) return;

        fireTime += Time.deltaTime;

        LookPlayer();

        if (nextFireTime <= fireTime && !enemyHealth_F.dead)
        {
            fireTime = 0;
            StartCoroutine(EnemyShoot());
            Attack();
        }
    }

    void LookPlayer()
    {
        //Vector3 direction = (player.position - Gun.position).normalized;
        Vector3 playerPosition = new Vector3((player.transform.position.x - Gun.transform.position.x), 0, (player.transform.position.z - Gun.transform.position.z));
        Quaternion lookRotation = Quaternion.LookRotation(playerPosition);
        Gun.rotation = Quaternion.Slerp(Gun.rotation, lookRotation, Time.deltaTime * speed);
        enemyGun.transform.LookAt(player.transform);
    }

    void Attack()
    {
        nextFireTime = Random.Range(minNextFireTime, maxNextFireTime);

        enemyAduio.PlayOneShot(enemyShootSound, 0.8f);
        // gunLine.SetPosition(0, enemyGun.transform.position);

        Ray ray = new Ray(enemyGun.transform.position, enemyGun.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            //gunLine.SetPosition(1, hit.point);

            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(damage);
            }
        }
    }

    void ClearEffect()
    {
        muzzle.SetActive(false);
    }

    IEnumerator EnemyShoot()
    {
        anim.SetTrigger("Attack");
        for (int i = 0; i < count; i++)
        {
            if (enemyHealth_F.dead) yield break;
            StartCoroutine(gunEffect());
            yield return new WaitForSeconds(brustTime);
        }
        anim.SetTrigger("Idle1");
    }

    IEnumerator gunEffect()
    {
        //gunLine.enabled = true;
        muzzle.SetActive(true);
        yield return new WaitForSeconds(disEffectTime);
        //gunLine.enabled = false;
        muzzle.SetActive(false);
    }
}
