using UnityEngine;
using System.Collections;

public class MainBody : MonoBehaviour {

	//Start
	void Start()
    {
	
	}
	
	//Update
	void Update()
    {
	
	}


    //Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the other thing that touch me is a laser, check for player script instead if possible
        if (other.GetComponent<PlayerLaser>())
        {
            //weaken
            GetComponentInParent<Enemy>().weakened = true;
            GameObject laserHit = (GameObject)Resources.Load("LaserHit");
            laserHit.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
            Instantiate(laserHit, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
    }

}
