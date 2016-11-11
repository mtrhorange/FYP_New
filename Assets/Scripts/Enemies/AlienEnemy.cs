using UnityEngine;
using System.Collections;

public class AlienEnemy : Enemy {

    public Vector2 target;

	//Start
	protected override void Start()
    {
        moveSpeed = 120f;
	}
	
	//Update
	protected override void Update()
    {
        Move();
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
}
