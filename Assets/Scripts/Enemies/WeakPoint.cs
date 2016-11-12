using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the other thing that touch me is a laser, check for player script instead if possible
        if (other.GetComponent<PlayerLaser>())
        {
            //damage
            GetComponentInParent<Enemy>().GetDamage(other.gameObject.GetComponent<PlayerLaser>().damage);
            GameObject laserHit = (GameObject)Resources.Load("LaserHit");
            laserHit.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
            Instantiate(laserHit, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
        }
    }

}
