using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    //laser prefab
	public GameObject eLaser, explosion;

    //health
	public bool weakened = false;
	protected int HP = 5;

    //movement
	public bool goDown = false, faceLeft;
    protected float moveSpeed = 60f;

    //attack
    public float attackTimer;
    protected float minTime = 3f, maxTime = 5f;


	//Start
	protected virtual void Start()
    {
        //random whether face up or down
        if (faceLeft)
        {
            transform.Rotate(Vector3.forward, -90f);
        }
        else if (!faceLeft)
        {
            transform.Rotate(Vector3.forward, 90f);
        }

        //attack timer
        setAttackTimer();
	}

	// Update is called once per frame
	protected virtual void Update()
    {
        Move();
	}

    //Move
    protected virtual void Move()
    {
        if (goDown)
        {
            if (faceLeft)
            {
                GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * moveSpeed;
            }
            if (transform.position.y < AIManager.instance.cameraBounds.min.y)
            {
                goDown = false;
            }
        }
        else
        {
            if (faceLeft)
            {
                GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * moveSpeed;
            }
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

    //Attack
    protected virtual void Attack()
    {
        GameObject temp = (GameObject)Instantiate(eLaser, transform.position, transform.rotation);
        temp.GetComponent<EnemyLaser>().damage = 1;
    }

	//called when this entity receives damage from another source
	public virtual void GetDamage(int dmg)
	{
		HP -= dmg;
		//check die
		if (HP <= 0)
		{
            explode();
            Destroy(this.gameObject);
		}
	}

    //set attack timer
    protected void setAttackTimer()
    {
        attackTimer = Random.Range(minTime, maxTime);
    }

    //On destroy
    protected virtual void OnDestroy()
    {
        
    }

    protected void explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}