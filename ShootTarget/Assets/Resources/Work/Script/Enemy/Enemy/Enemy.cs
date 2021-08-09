using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI
using UnityEngine.UI;    //使用UnityEngine.AI

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;    //宣告NavMeshAgent
    public GameObject target_obj;    //目標物件
    public Animator anim_Enemy_Walk;

    public CoverPosition[] coverPositions;
    public CoverPosition coverPosition;

    public bool IsInPosition;

    public Collider[] enemy_Collider;

    Enemy_Attack enemy_Attack;
    Enemy_Health enemy_Health;

    public float FindCoverTime = 0;
    public float FindPlayerTime = 0;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target_obj = GameObject.Find("Player");
        anim_Enemy_Walk = GetComponent<Animator>();
        enemy_Attack = GetComponent<Enemy_Attack>();
        enemy_Health = GetComponent<Enemy_Health>();
        enemy_Collider = GetComponentsInChildren<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckRespawnsPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_Health.dead)
        {
            agent.isStopped = true;
            anim_Enemy_Walk.SetBool("Moveing", false);
            return;
        }

        if (coverPosition == null)
        {
            FindPlayer();
        }
        else
        {
            FindCover();
        }
    }

    void CheckRespawnsPoint()
    {
        coverPositions = (CoverPosition[])GameObject.FindObjectsOfType(typeof(CoverPosition));

        float distanceMin = float.MaxValue;
        for (int i = 0; i < coverPositions.Length; i++)
        {
            float distCheck = Vector3.Distance(coverPositions[i].transform.position, this.transform.position);

            if (coverPositions[i] == coverPositions[i].IsNull && coverPositions[i].CanHide)
            {
                if (distCheck < distanceMin)
                {
                    distanceMin = distCheck;
                    coverPosition = coverPositions[i];
                }
            }
        }

        if (coverPosition != null)
            coverPosition.IsNull = false;
    }

    void FindCover()
    {
        if (FindCoverTime > 5) return;

        if (!IsInPosition)
        {
            agent.SetDestination(coverPosition.transform.position);

            if (enemy_Attack.IsShooting)
            {
                anim_Enemy_Walk.SetBool("Moveing", false);
                agent.isStopped = true;
            }
            else
            {
                anim_Enemy_Walk.SetBool("Moveing", true);
                agent.isStopped = false;
            }
        }
        else if (IsInPosition)
        {
            FindCoverTime += Time.unscaledDeltaTime;
            anim_Enemy_Walk.SetBool("Moveing", false);
            agent.isStopped = true;
        }
    }

    void FindPlayer()
    {
        if (FindPlayerTime > 5) return;
        agent.stoppingDistance = 7f;
        agent.SetDestination(target_obj.transform.position);
        float Distance = Vector3.Distance(target_obj.transform.position, transform.position);

        if (Distance / 2 > agent.stoppingDistance)
        {
            if (enemy_Attack.IsShooting)
            {
                anim_Enemy_Walk.SetBool("Moveing", false);
                agent.isStopped = true;
            }
            else
            {
                anim_Enemy_Walk.SetBool("Moveing", true);
                agent.isStopped = false;
            }
        }
        else
        {
            FindPlayerTime += Time.unscaledDeltaTime;
            anim_Enemy_Walk.SetBool("Moveing", false);
            agent.isStopped = true;
        }
    }

    public void ClearCollider()
    {
        for (int i = 0; i < enemy_Collider.Length; i++)
        {
            enemy_Collider[i].enabled = false;
        }
    }
}
