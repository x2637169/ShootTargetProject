using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    Animator anim_Enemy_Walk_Health;
    ScoreManager scoreManager;
    SlowMotionPower slowMotionPower;

    public List<float> distScore;
    public List<float> distNum;
    public GameObject player;

    public float enemyHealth = 100;
    public int slowMotionSelectionC;

    AudioSource enemy_Walk_Health_Audio;
    public AudioClip deadSound;

    public bool dead;

    Enemy enemy;
    Enemy_Attack ememy_Attack;

    // Start is called before the first frame update
    void Start()
    {
        anim_Enemy_Walk_Health = GetComponent<Animator>();
        enemy_Walk_Health_Audio = GetComponent<AudioSource>();
        enemy = GetComponent<Enemy>();
        ememy_Attack = GetComponent<Enemy_Attack>();
        scoreManager = GameObject.FindWithTag("Manager").GetComponent<ScoreManager>();
        slowMotionPower = GameObject.FindWithTag("SlowMotionManager").GetComponent<SlowMotionPower>();
        player = GameObject.FindWithTag("Player");

        FloatingTextController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage, int slowMotionSelection)
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        for (int i = 0; i < distNum.Count; i++)
        {
            if (distNum[i] < dist)
            {
                damage = distScore[i] * damage;
            }
        }

        scoreManager.GetScore((int)damage);
        FloatingTextController.CreateFloatingText(damage.ToString("f0"), transform);

        slowMotionSelectionC = slowMotionSelection;
        enemyHealth -= damage;
        enemy_Walk_Health_Audio.PlayOneShot(deadSound, 1.5f);

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            StartCoroutine(EnemyDead());
        }
    }

    public IEnumerator EnemyDead()
    {
        if (dead) yield break;
        dead = true;

        if (enemy.coverPosition != null)
            enemy.coverPosition.ResetNull();

        enemy.ClearCollider();
        ememy_Attack.ClearEffect();

        anim_Enemy_Walk_Health.SetTrigger("Dead");

        slowMotionPower.SlowMotionChance(slowMotionSelectionC);

        Destroy(gameObject, 3);
    }
}
