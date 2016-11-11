using UnityEngine;
using System.Collections;

public class BiEnemy : Enemy {

    //Start
    protected override void Start()
    {
        //bi enemy properties
        HP = 1;
        moveSpeed = 35f;

        transform.Rotate(Vector3.forward, 90f);

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
        GameObject temp = (GameObject)Instantiate(eLaser, transform.position, transform.rotation);
        temp.GetComponent<EnemyLaser>().damage = 1;
        temp = (GameObject)Instantiate(eLaser, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 270));
        temp.GetComponent<EnemyLaser>().damage = 1;
    }
}
