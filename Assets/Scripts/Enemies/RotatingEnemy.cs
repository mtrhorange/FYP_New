using UnityEngine;
using System.Collections;

public class RotatingEnemy : Enemy
{

    public float rotateSpeed = 35f;


	// Use this for initialization
	protected override void Start ()
    {
        HP = 2;

        setAttackTimer();
	}

    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.rotation =
            Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + Time.deltaTime * rotateSpeed));
        if (goDown)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 0.018f * moveSpeed,
                transform.position.z);

            if (transform.position.y < AIManager.instance.cameraBounds.min.y)
            {
                goDown = false;
            }
        }
        else
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 0.018f * moveSpeed,
                transform.position.z);

            if (transform.position.y > AIManager.instance.cameraBounds.max.y)
            {
                goDown = true;
            }
        }

        //attack once timer is over
        if (attackTimer <= 0)
        {
            Attack();
            //reset timer
            setAttackTimer();
        }

        attackTimer -= Time.deltaTime;
    }
}
