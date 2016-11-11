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
        if (other.gameObject.tag == "PlayerLaser")
        {
            //weaken
            GetComponentInParent<Enemy>().weakened = true;

            Destroy(other.gameObject);
        }
    }

}
