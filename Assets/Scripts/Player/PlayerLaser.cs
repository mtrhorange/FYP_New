using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

    private float lifeTime = 3f;
	public int damage = 1;

	// Use this for initialization
	void Start () {
		//GetComponent<Rigidbody2D>().velocity = transform.up * Time.deltaTime * 600f;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.up * Time.deltaTime * 18f;

        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
        lifeTime -= Time.deltaTime;
	}
}
