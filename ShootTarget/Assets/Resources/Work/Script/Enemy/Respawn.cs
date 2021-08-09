using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    EnemyManager enemyManger;

    public GameObject enemy;
    public int num;

    public float enemySpawnTime;
    public float enemySpawnDeltaTime;

    void Awake()
    {
        enemyManger = GameObject.FindWithTag("Manager").GetComponent<EnemyManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        enemySpawnDeltaTime += Time.deltaTime;
        if (enemySpawnDeltaTime > enemySpawnTime)
        {
            SpawnEnemy();
            enemySpawnDeltaTime = 0;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);
        enemyManger.RespawnTime(num);
    }
}
