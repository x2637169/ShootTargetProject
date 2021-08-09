using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Respawn[] respawns;

    public List<int> minusEnemyTime = new List<int>();
    public List<int> minusEnemyMinTime = new List<int>();
    public List<int> minusEnemyMaxTime = new List<int>();
    public int EnemyCount;

    public List<int> minusAirEnemyTime = new List<int>();
    public List<int> minusAirEnemyMinTime = new List<int>();
    public List<int> minusAirEnemyMaxTime = new List<int>();
    public int airEnemyCount;

    public GameObject airEnemy;

    public float enemyMinTime, enemyMaxTime;

    public float airEnemyMinTime, airEnemyMaxTime;

    private float instantiateAirEnemyTime;
    private float instantiateAirEnemyDeltaTime;

    TimeManager timeManager;

    void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        respawns = FindObjectsOfType(typeof(Respawn)) as Respawn[];
        instantiateAirEnemyTime = Random.Range(airEnemyMinTime, airEnemyMaxTime);
        FirstTimeTransRespawnTime();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        MinusAirEnemyRepawnTime();
        InstantiateAirEnemy();
    }

    public void FirstTimeTransRespawnTime()
    {
        for (int i = 0; i < respawns.Length; i++)
        {
            respawns[i].num = i;
            respawns[i].enemySpawnTime = Random.Range(5, 20);
        }

    }

    public void RespawnTime(int i)
    {
        respawns[i].enemySpawnTime = Random.Range(enemyMinTime, enemyMaxTime);
    }

    void InstantiateAirEnemy()
    {
        instantiateAirEnemyDeltaTime += Time.unscaledDeltaTime;

        if (instantiateAirEnemyTime < instantiateAirEnemyDeltaTime)
        {
            Instantiate(airEnemy, new Vector3(Random.Range(20, 35), 2, Random.Range(10, 20)), airEnemy.transform.rotation);
            instantiateAirEnemyDeltaTime = 0;
            instantiateAirEnemyTime = Random.Range(airEnemyMinTime, airEnemyMaxTime);
        }
    }

    void MinusAirEnemyRepawnTime()
    {
        for (EnemyCount = EnemyCount; EnemyCount < minusEnemyTime.Count && timeManager.totalTime < minusEnemyTime[EnemyCount]; EnemyCount++)
        {
            enemyMinTime -= minusEnemyMinTime[EnemyCount];
            enemyMaxTime -= minusEnemyMaxTime[EnemyCount];
        }

        for (airEnemyCount = airEnemyCount; airEnemyCount < minusAirEnemyTime.Count && timeManager.totalTime < minusAirEnemyTime[airEnemyCount]; airEnemyCount++)
        {
            airEnemyMinTime -= minusAirEnemyMinTime[airEnemyCount];
            airEnemyMaxTime -= minusAirEnemyMaxTime[airEnemyCount];
        }
    }
}
