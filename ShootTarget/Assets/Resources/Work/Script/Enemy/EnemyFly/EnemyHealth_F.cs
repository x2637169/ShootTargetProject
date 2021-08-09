using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_F : MonoBehaviour
{
    public float enemyHealth = 100;
    public float downSpeed;
    public GameObject parachute;

    public GameObject allGameObject;
    AudioSource enemyAudio;
    public AudioClip enemySound;
    Animator anim;
    Gun gun;
    Enemy_F enemy_F;
    public SlowMotionPower slowMotionPower;
    public ScoreManager scoreManager;

    public bool dead;
    public bool onFloor;

    public int slowMotionSelectionC;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gun = GetComponent<Gun>();
        enemy_F = GetComponent<Enemy_F>();
        enemyAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.FindWithTag("Manager").GetComponent<ScoreManager>();
        slowMotionPower = GameObject.FindWithTag("SlowMotionManager").GetComponent<SlowMotionPower>();

        FloatingTextController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            Down();
        }
    }

    public void Down()
    {
        if (onFloor) return;
        Vector3 Posp = new Vector3(parachute.transform.position.x, parachute.transform.position.y - downSpeed * Time.deltaTime, parachute.transform.position.z);
        parachute.transform.position = Posp;
    }

    public void TakeDamage(float damage, int slowMotionSelection)
    {
        slowMotionSelectionC = slowMotionSelection;

        scoreManager.GetScore((int)damage);

        FloatingTextController.CreateFloatingText(damage.ToString(), transform);

        enemyHealth -= damage;

        enemyAudio.PlayOneShot(enemySound, 1.5f);

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

        enemy_F.StopAnimation();

        anim.SetTrigger("Dead");

        slowMotionPower.SlowMotionChance(slowMotionSelectionC);

        yield return new WaitForSeconds(4f);

        Destroy(allGameObject);
    }
}