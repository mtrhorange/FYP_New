using UnityEngine;
using System.Collections;

public class Boss : Enemy {

    public GameObject turret1, turret2, gunL1, gunL2, gunR1, gunR2;
    public GameObject weakPt1, weakPt2;
    public bool isWkPt1Destroyed = false, isWkPt2Destroyed = false;

    public bool lookLeft = true;

	// Use this for initialization
	protected override void Start () {
        HP = 300;
        moveSpeed = 0f;

        minTime = 1f;
        maxTime = 2f;

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
        //left side
        float angle = Random.Range(45f, 135f);
        Quaternion here = Quaternion.AngleAxis(angle, Vector3.forward);
        //gun 1
        Instantiate(eLaser, gunL1.transform.position, here);
        angle = Random.Range(45f, 135f);
        here = Quaternion.AngleAxis(angle, Vector3.forward);
        //gun 2
        Instantiate(eLaser, gunL2.transform.position, here);

        //right side
        angle = Random.Range(225f, 315f);
        here = Quaternion.AngleAxis(angle, Vector3.forward);
        //gun 1
        Instantiate(eLaser, gunR1.transform.position, here);
        angle = Random.Range(225f, 315f);
        here = Quaternion.AngleAxis(angle, Vector3.forward);
        //gun 2
        Instantiate(eLaser, gunR2.transform.position, here);
    }

    public void organiseTurn()
    {
        if (turret1)
        {
            turret1.GetComponent<BossSide>().turn();
        }
        if (turret2)
            turret2.GetComponent<BossSide>().turn();
    }

    //Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the other thing that touch me is a player laser
        if (other.GetComponent<PlayerLaser>())
        {
            //damage
            this.GetDamage(other.gameObject.GetComponent<PlayerLaser>().damage);
            GameObject laserHit = (GameObject)Resources.Load("LaserHit");
            laserHit.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
            Instantiate(laserHit, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
        }
    }


    //request shield
    public void CallShield(GameObject wkPt)
    {
        if (!isWkPt1Destroyed && !isWkPt2Destroyed)
        {
            weakPt1.GetComponent<WeakPoint>().DisableShield();
            weakPt1 = weakPt2;
            weakPt2 = wkPt;
            weakPt2.GetComponent<WeakPoint>().EnableShield();
        }
        else if (!isWkPt2Destroyed)
        {
            weakPt1.GetComponent<WeakPoint>().DisableShield();
            weakPt1 = wkPt;
            weakPt1.GetComponent<WeakPoint>().EnableShield();
        }

    }

    public void BossSideDestroyed()
    {
        if (!isWkPt1Destroyed)
        {
            weakPt1.GetComponent<WeakPoint>().DisableShield();
            weakPt1 = weakPt2;
            weakPt1.GetComponent<WeakPoint>().EnableShield();
            isWkPt1Destroyed = true;
        }
        else
        {
            weakPt1.GetComponent<WeakPoint>().DisableShield();
            isWkPt2Destroyed = true;
        }


    }

    //onDestroy
    public void OnDestroy()
    {
        GameManager.instance.Won();
    }
}
