using UnityEngine;
using System.Collections;

public class BiEnemy : Enemy {

    //Start
    protected override void Start()
    {
        //bi enemy properties
        HP = 1;
        moveSpeed = 35f;

        //attack timer
        setAttackTimer();
    }

    //Update
    protected override void Update()
    {
        base.Update();
    }

    //Attack
    protected override void Attack()
    {
        Instantiate(eLaser, transform.position, transform.rotation);
        Instantiate(eLaser, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, 90f, transform.rotation.w));
    }
}
