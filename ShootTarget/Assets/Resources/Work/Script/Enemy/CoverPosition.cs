using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPosition : MonoBehaviour
{
    public bool IsNull;
    public bool CanHide;
    public bool OnFloor;

    public Enemy enemy;
    public Enemy_Health enemy_Health;
    public Enemy_Attack enemy_Attack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (IsNull) return;

        IsNull = false;

        enemy = other.gameObject.GetComponentInParent<Enemy>();
        enemy_Health = other.gameObject.GetComponentInParent<Enemy_Health>();

        if (OnFloor)
        {
            enemy_Attack = other.gameObject.GetComponentInParent<Enemy_Attack>();
            enemy_Attack.CanShoot = true;
        }

        if (enemy != null)
            enemy.IsInPosition = true;
    }

    public void ResetNull()
    {
        IsNull = true;
    }
}
