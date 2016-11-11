using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    //laser prefab
	public GameObject eLaser;

    //health
	internal bool weakened = false;
	private int HP = 1;

    //movement
	private bool goLeft = false, faceDown = false;
    private float moveSpeed = 60f;

    //attack
    public float attackTimer;
    private float minTime = 5f, maxTime = 8f;


	//Start
	void Start()
    {
        //random whether face up or down
        if (Random.Range(0, 2) == 1)
        {
            transform.Rotate(Vector3.forward, 180f);
            faceDown = true;
        }

        //attack timer
        setAttackTimer();
	}

	// Update is called once per frame
	void Update () {
		if (goLeft)
		{
            if (faceDown)
            {
                GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * moveSpeed;
            }
			if (transform.position.x < AIManager.instance.cameraBounds.min.x)
			{
				goLeft = false;
			}
		}
		else
		{
            if (faceDown)
            {
                GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * moveSpeed;
            }
            if (transform.position.x > AIManager.instance.cameraBounds.max.x)
			{
				goLeft = true;
			}
		}


        //attack once timer is over
        if (attackTimer <= 0)
        {
            Instantiate(eLaser, transform.position, transform.rotation);

            //reset timer
            setAttackTimer();
        }

        attackTimer -= Time.deltaTime;
	}


	//called when this entity receives damage from another source
	internal void GetDamage(int dmg)
	{
		HP -= dmg;
		//check die
		if (HP <= 0)
		{
			Destroy(this.gameObject);
		}
	}

    //set attack timer
    private void setAttackTimer()
    {
        attackTimer = Random.Range(minTime, maxTime);
    }
}