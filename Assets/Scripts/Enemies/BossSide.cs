using UnityEngine;
using System.Collections;

public class BossSide : Boss {

    private int shots = 0;
    private bool shooting = false;
    private float shootTimer = 0f;

    private bool turningSoon = false;
    private float secondsToTurn = 0f;


	// Use this for initialization
	protected override void Start () {
        HP = 10;
	}
	
	// Update is called once per frame
	protected void Update () {
        if (shooting)
        {
            if (shootTimer <= 0)
            {
                if (shots < 3)
                {
                    trueAttack();
                    shootTimer = 0.5f;
                    shots++;
                }
                else
                {
                    shooting = false;
                    shootTimer = 0f;
                    shots = 0;
                }
            }
            shootTimer -= Time.deltaTime;
        }


        //if turning soon
        if (turningSoon)
        {
            Debug.Log(secondsToTurn);
            secondsToTurn -= Time.deltaTime;
            if (secondsToTurn <= 0)
            {
                //Quaternion here = Quaternion.EulerAngles(0f, 0f, lookLeft ? 270f : 90f);
                //transform.rotation = Quaternion.Slerp(transform.rotation, here, Time.deltaTime * 4f);

                transform.rotation =
            Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + Time.deltaTime * 30f));


                if ((transform.rotation.z.ToString("F0") == "0"))
                {
                    turningSoon = false;
                    transform.parent.gameObject.GetComponent<Boss>().lookLeft = lookLeft;
                }
                else if ((transform.rotation.z.ToString("F0") == "180"))
                {
                    turningSoon = false;
                    transform.parent.gameObject.GetComponent<Boss>().lookLeft = lookLeft;
                }
            }
        }



        base.Update();
	}

    //Attack
    protected override void Attack()
    {
        shooting = true;
    }

    //Attack 4 realzies
    private void trueAttack()
    {
        //get rotation towards player on this side
        Vector2 tgt = transform.parent.GetComponent<Boss>().lookLeft ? GameManager.instance.player1.transform.position : GameManager.instance.player2.transform.position;

        Vector3 look = (tgt - new Vector2(transform.position.x, transform.position.y)).normalized;
        float angle = ((Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg) - 90f);
        Quaternion here = Quaternion.AngleAxis(angle, Vector3.forward);


        GameObject temp = (GameObject)Instantiate(eLaser, transform.position, here);
        temp.GetComponent<EnemyLaser>().damage = 1;
    }

    //get damage
    public override void GetDamage(int dmg)
    {
        HP -= dmg;
        Debug.Log("Got hit");
        transform.parent.gameObject.GetComponent<Boss>().organiseTurn();

        //check die
        if (HP <= 0)
        {
            explode();
            Destroy(this.gameObject);
        }
    }

    //time to turn soon
    public void turn()
    {
        Debug.Log("turn");
        if (!turningSoon)
        {
            Debug.Log("turning soon");
            turningSoon = true;
            secondsToTurn = 3f;
            lookLeft = !lookLeft;
        }
    }
}
