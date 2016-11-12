using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour
{
    public List<GameObject> powerUpList;
    public float timeToNext;
    public float randomTimeMin = 3f;
    public float randomTimeMax = 7f;
    

    // Use this for initialization
    void Start ()
	{
	    timeToNext = Random.Range(randomTimeMin, randomTimeMax);
	}
	
	// Update is called once per frame
	void Update ()
	{
        timeToNext -= Time.deltaTime;

	    if (timeToNext <= 0)
	    {
	        GameObject temp = (GameObject)Instantiate(powerUpList[Random.Range(0, 4)], transform.position, Quaternion.identity);

	        temp.GetComponent<Powerup>().direction = getDirection();


            timeToNext = Random.Range(randomTimeMin, randomTimeMax);
        }

	}

    private Vector3 getDirection()
    {
        Vector3 direction;

        if (Random.Range(0, 2) == 0)
        {
            direction = transform.up * Random.Range(0f, 1f);
        }
        else
        {
            direction = -transform.up * Random.Range(0f, 1f);
        }

        if (Random.Range(0, 2) == 0)
        {
            direction += transform.right ;

        }
        else
        {
            direction -= transform.right;
        }

        return direction;
    }
}
