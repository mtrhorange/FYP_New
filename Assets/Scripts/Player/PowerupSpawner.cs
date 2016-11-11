using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {

	float spawnTimer = 30;

	public GameObject shieldPrefab, splitPrefab, healPrefab, laserPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;

		if (spawnTimer >= 4f) {
			int rand = Random.Range (0, 4);
			if (rand == 0) {
				GameObject power = (GameObject)Instantiate (shieldPrefab, transform.position, Quaternion.identity);

			}
			else if (rand == 1) {
				GameObject power = (GameObject)Instantiate (splitPrefab, transform.position, Quaternion.identity);

			}
			else if (rand == 2) {
				GameObject power = (GameObject)Instantiate (healPrefab, transform.position, Quaternion.identity);
			
			}
			else if (rand == 3) {
				GameObject power = (GameObject)Instantiate (laserPrefab, transform.position, Quaternion.identity);

			}

			spawnTimer = 0f;
		}
	}
}
