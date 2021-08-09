using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public enum hitbox { head, body };
    public hitbox hitBoxType;

    public enum HealthSelection { Enemy, Enemy_F };
    public HealthSelection healthType;

    public int head;
    public int body;

    public float slowMotionMultiple = 2;
    public int slowMotionSelection;

    Enemy_Health enemy_Health;
    EnemyHealth_F enemyHealth_F;

    SlowMotionPower slowMotion;
    PlayerHealth playerHealth;
    void Awake()
    {
        slowMotion = GameObject.FindWithTag("SlowMotionManager").GetComponent<SlowMotionPower>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        switch (healthType)
        {
            case HealthSelection.Enemy:
                enemy_Health = GetComponentInParent<Enemy_Health>();
                break;
            case HealthSelection.Enemy_F:
                enemyHealth_F = GetComponentInParent<EnemyHealth_F>();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (hitBoxType == hitbox.head)
        {
            damage *= head;
            slowMotionSelection = 1;
        }
        else if (hitBoxType == hitbox.body)
        {
            damage *= body;
            slowMotionSelection = 2;
        }

        if (slowMotion.SlowMotionOn)
        {
            damage = damage * slowMotionMultiple;
            playerHealth.AddHealth(15);
        }

        switch (healthType)
        {
            case HealthSelection.Enemy:
                enemy_Health.TakeDamage(damage, slowMotionSelection);
                break;
            case HealthSelection.Enemy_F:
                enemyHealth_F.TakeDamage(damage, slowMotionSelection);
                break;
        }
    }
}
