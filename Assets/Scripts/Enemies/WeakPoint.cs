﻿using UnityEngine;
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

            Destroy(other.gameObject);
        }
    }

}
