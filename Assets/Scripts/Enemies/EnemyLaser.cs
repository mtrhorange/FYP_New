using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour
{
    private float lifeTime = 10f;
    public int damage;

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = transform.up * Time.deltaTime * 80f;
	}

	// Update is called once per frame
	void Update()
	{
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
        lifeTime -= Time.deltaTime;
	}
}