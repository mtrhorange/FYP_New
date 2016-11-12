using UnityEngine;
using System.Collections;

public class AlienEnemy : Enemy {

    public Vector2 target;
    public Sprite dead;
    private bool isDead = false;

	//Start
	protected override void Start()
    {
        moveSpeed = 120f;
        HP = 1;
	}
	
	//Update
	protected override void Update()
    {
        if (!isDead)
        {
            Move();
        }
	}

    //Move
    protected override void Move()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Time.deltaTime * moveSpeed;

        Vector3 look = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
        float angle = ((Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg) - 90f);
        Quaternion here = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, here, Time.deltaTime * 0.5f);

        if (transform.position.x <= AIManager.instance.cameraBounds.min.x || transform.position.x >= AIManager.instance.cameraBounds.max.x)
        {
            Destroy(this.gameObject);
        }
    }

    //get damage
    public override void GetDamage(int dmg)
    {
        HP -= dmg;
        //check die
        if (HP <= 0)
        {
            isDead = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().sprite = dead;
            GetComponentInChildren<PolygonCollider2D>().enabled = false;
            Destroy(this.gameObject, 1f);
        }
    }
}
