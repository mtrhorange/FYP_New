using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {

    public bool isBoss = false;
    public Boss boss;
    public GameObject shield;

    bool callingShield = false;
    float shieldCallTimer = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (callingShield)
        {
            shieldCallTimer += Time.deltaTime;
            if (shieldCallTimer >= 3.0f)
            {
                shieldCallTimer = 0f;
                callingShield = false;
                boss.CallShield(gameObject);
            }

        }
	}

    //Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the other thing that touch me is a laser, check for player script instead if possible
        if (other.GetComponent<PlayerLaser>())
        {
            //damage
            transform.parent.GetComponent<Enemy>().GetDamage(other.gameObject.GetComponent<PlayerLaser>().damage);
            GameObject laserHit = (GameObject)Resources.Load("LaserHit");
            laserHit.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
            Instantiate(laserHit, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);

            if (isBoss)
            {
                callingShield = true;


            }

        }
    }

    public void EnableShield()
    {
        shield.SetActive(true);
    }

    public void DisableShield()
    {
        shield.SetActive(false);
    }

}
