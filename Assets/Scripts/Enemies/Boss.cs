using UnityEngine;
using System.Collections;

public class Boss : Enemy {

    public GameObject turret1, turret2;

    public bool lookLeft = true;

	// Use this for initialization
	protected override void Start () {
        HP = 100;
        moveSpeed = 0f;

        //attack timer
        setAttackTimer();
	}
	
	// Update is called once per frame
	protected override void Update () {
        //attack once timer is over
        if (attackTimer <= 0)
        {
            Attack();
            //reset timer
            setAttackTimer();
        }

        attackTimer -= Time.deltaTime;
	}

    //Attack
    protected override void Attack()
    {
        Debug.Log("ATTACK, BOSS");
        if (turret1)
            turret1.GetComponent<BossSide>().Attack();
        if (turret2)
            turret2.GetComponent<BossSide>().Attack();
    }

    public void organiseTurn()
    {
        if (turret1)
        {
            turret1.GetComponent<BossSide>().turn();
            Debug.Log("turret1");
        }
        if (turret2)
            turret2.GetComponent<BossSide>().turn();
        Debug.Log("Organise turn");
    }
}
