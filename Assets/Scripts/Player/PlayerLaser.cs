using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = transform.up * Time.deltaTime * 600f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
