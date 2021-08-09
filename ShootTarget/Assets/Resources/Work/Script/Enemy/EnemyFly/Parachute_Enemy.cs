using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute_Enemy : MonoBehaviour
{
    public Animator anim;
    public GameObject frame;

    EnemyHealth_F enemyHealth_F;

    // Start is called before the first frame update
    void Start()
    {
        frame.gameObject.SetActive(true);

        enemyHealth_F = GetComponentInChildren<EnemyHealth_F>();

        //anim = GetComponent<Animator>();
        anim.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ClearFrame();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            anim.enabled = false;
            enemyHealth_F.onFloor = true;
            frame.gameObject.SetActive(false);

            Collider parachuteCollider = GetComponent<Collider>();
            parachuteCollider.enabled = false;

            Rigidbody parachuteRig = GetComponent<Rigidbody>();
            Destroy(parachuteRig);
        }
    }

    void ClearFrame()
    {
        if (enemyHealth_F.dead)
        {
            anim.enabled = false;
            frame.gameObject.SetActive(false);
        }
    }
}
